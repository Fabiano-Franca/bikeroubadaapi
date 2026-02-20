using BikeRoubada.Api.AuxiliaryModels;
using BikeRoubada.Api.Configurations;
using BikeRoubada.Api.Data;
using BikeRoubada.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Net.Mail;
using System.Text;



internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configura a porta dinamicamente para o Railway
        var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
        builder.WebHost.UseUrls($"http://*:{port}");

        // Add services to the container.
        builder.Services.AddControllers()
            .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Insira o token dessa maneira: Bearer {seu token}",
                Name = "Authorization",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                        });
        });


        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // 1. Tenta obter a string de conexão
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // Priorizamos a MYSQL_URL do Railway (que agora será a pública)
        var envUrl = Environment.GetEnvironmentVariable("MYSQL_URL");

        if (!string.IsNullOrEmpty(envUrl))
        {
            // O Railway as vezes envia "mysql://" no início, o Uri suporta isso.
            var uri = new Uri(envUrl);
            var db = uri.AbsolutePath.Trim('/');
            var userPass = uri.UserInfo.Split(':');

            // AJUSTE: Adicionamos parâmetros de compatibilidade para MySQL 8/9
            // SslMode=None é essencial se você não configurou certificados SSL no Railway
            connectionString = $"Server={uri.Host};Port={uri.Port};Database={db};Uid={userPass[0]};Pwd={userPass[1]};SslMode=None;AllowPublicKeyRetrieval=True;Connect Timeout=60;Charset=utf8;";

            Console.WriteLine($"--- Conectando ao Host: {uri.Host} na Porta: {uri.Port} ---");
        }

        // 2. Configuração do DbContext com Versão Fixa e Retry
        var serverVersion = new MySqlServerVersion(new Version(9, 4, 0));

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(connectionString, serverVersion, x =>
            {
                x.UseNetTopologySuite();
                // Isso faz a API esperar o banco subir se houver lag de rede no Railway
                x.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            });
        });

        // Configuração do Contexto 2 (Api/Identity) - VERIFIQUE SE ESTÁ ASSIM
        builder.Services.AddDbContext<ApiDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        builder.Services.ResolveDependencies();
        builder.Services.AddIdentityConfiguration(builder.Configuration);

        builder.Services
                    .AddFluentEmail("oliveirasamuca1971@gmail.com")
                    .AddRazorRenderer()
                    .AddSmtpSender(new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("oliveirasamuca1971@gmail.com", "vtlp aogy pdwm tigp"),
                        EnableSsl = true
                    });

        var JwtSettings = builder.Configuration.GetSection("JwtSettings");
        builder.Services.Configure<JwtSettings>(JwtSettings);
        var jwtSettings = JwtSettings.Get<JwtSettings>();
        var key = Encoding.ASCII.GetBytes(jwtSettings.Segredo);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = true;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = jwtSettings.Audiencia,
                ValidIssuer = jwtSettings.Emissor
            };
        });

        //CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Development",
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

            options.AddPolicy("Production",
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

            //options.AddPolicy("Production",
            //    builder =>
            //        builder
            //            .WithMethods("GET")
            //            //.WithOrigins("http://desenvolvedor.io")
            //            .WithOrigins("http://sanatorio")
            //            .SetIsOriginAllowedToAllowWildcardSubdomains()
            //            //.WithHeaders(HeaderNames.ContentType, "x-custom-header")
            //            .AllowAnyHeader());
        });

        builder.Services.Configure<KestrelServerOptions>(options =>
        {
            options.Limits.MaxRequestBodySize = 100 * 1024 * 1024; // 100MB
        });


        var app = builder.Build();

        // 2. Bloco de Migração Automática
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                // 1. Migra o contexto de Dados (Negócio)
                var dbData = services.GetRequiredService<AppDbContext>();
                dbData.Database.Migrate();

                // 2. Migra o contexto da API (Identity/Auth)
                // Substitua 'ApiDbContext' pelo nome real da classe no seu projeto .Api
                var dbApi = services.GetRequiredService<ApiDbContext>();
                dbApi.Database.Migrate();

                Console.WriteLine("--- Todas as migrações (Data e Api) aplicadas com sucesso! ---");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao aplicar migrações: {ex.Message}");
            }
        }

        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
        //    app.UseCors("Development");
        //    app.UseSwagger();
        //    app.UseSwaggerUI();
        //}

        // Deixe assim para funcionar no Railway:
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bike Roubada API V1");
            c.RoutePrefix = string.Empty; // Isso faz o Swagger abrir direto na URL principal
        });

        //app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}