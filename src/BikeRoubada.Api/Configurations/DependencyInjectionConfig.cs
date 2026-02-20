using BikeRoubada.Api.Utilities;
using BikeRoubada.Business.Interfaces;
using BikeRoubada.Business.Notificacoes;
using BikeRoubada.Business.Services;
using BikeRoubada.Data.Context;
using BikeRoubada.Data.Repository;

namespace BikeRoubada.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        
        
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            //Data
            services.AddScoped<AppDbContext>();
            services.AddScoped<IAlertaRepository, AlertaRepository>();
            services.AddScoped<IArquivoRepository, ArquivoRepository>();
            services.AddScoped<IBicicletaRepository, BicicletaRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IRouboRepository, RouboRepository>();
            services.AddScoped<ITipoAlertaRepository, TipoAlertaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            //Business
            services.AddScoped<IAlertaService, AlertaService>();
            services.AddScoped<IArquivoService, ArquivoService>();
            services.AddScoped<IBicicletaService, BicicletaService>();
            services.AddScoped<IEnderecoService, EnderecoService>();
            services.AddScoped<IRouboService, RouboService>();
            services.AddScoped<ITipoAlertaService, TipoAlertaService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<INotificador, Notificador>();

            services.AddSingleton<IFileHandler, FileHandler>();
            services.AddSingleton<IEmailSender, EmailSender>();



            return services;
        }
    }
}
