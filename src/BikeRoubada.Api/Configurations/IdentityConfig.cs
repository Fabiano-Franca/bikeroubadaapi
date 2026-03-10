using BikeRoubada.Api.Data;
using BikeRoubada.Api.Utilities;
using BikeRoubada.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BikeRoubada.Api.Configurations
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApiDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), options => options.UseNetTopologySuite());
            });

            services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApiDbContext>()
                .AddErrorDescriber<TradutorMensagensIdentity>()
                .AddDefaultTokenProviders();
            return services;
        }
    }
}
