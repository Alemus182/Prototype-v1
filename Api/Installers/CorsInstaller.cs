using System;
using Api.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FullNetCore.Installers
{
    public class CorsInstaller : IInstaller
    {
        readonly string AllowSpecificOrigins = "_myAllowSpecificOrigins";

        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddCors(op =>
            {
                op.AddPolicy(
                    name: AllowSpecificOrigins,
                        builder =>
                        { builder
                              .AllowAnyHeader()
                              .AllowAnyMethod() 
                              .AllowAnyOrigin();
                        });
            });

        }
    }
}
