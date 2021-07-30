using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Infraestructure.Data;
using Infraestructure.Models;
using Application.Interfaces.Infraestructure.Data.Base;
using Application.Interfaces.Infraestructure.Data;
using Infraestructure.Services;
using Application.Interfaces.Services;
using Application.Interfaces.Infraestructure.Services.Properties;
using Infraestructure.Services.SIIC;

namespace Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // configuratión data access
            var connectionStringBase = configuration["ConnectionStrings:PrototypeConnection"];

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped<ISprocRepository, SprocRepository>();

            services.AddDbContext<PropertyContext>(c => c.UseSqlServer(connectionStringBase));

            services.AddTransient<IDbConnection>((sp) => new SqlConnection(connectionStringBase));

            // Properties services and repositories
            services.AddScoped<IPropertiesRepository, PropertiesRepository>();
            services.AddScoped<IFilePropertiesService, FilePropertiesService>();

            // Auth services
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
