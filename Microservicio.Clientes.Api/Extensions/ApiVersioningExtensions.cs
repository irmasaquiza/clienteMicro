using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace Microservicio.Clientes.Api.Extensions
{
    public static class ApiVersioningExtensions
    {
        public static IServiceCollection AddApiVersioningConfig(this IServiceCollection services)
        {
            // 🔥 CONFIGURACIÓN BASE DE VERSIONADO
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);

                options.AssumeDefaultVersionWhenUnspecified = true;

                options.ReportApiVersions = true;

                // 🔥 leer versión desde la URL: /api/v1/...
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            // 🔥 EXPLORER PARA SWAGGER 💣
            services.AddApiVersioning().AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV"; // v1, v2
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}