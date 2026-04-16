using Microsoft.EntityFrameworkCore;
using Microservicio.Clientes.DataAccess.Context;
using Microservicio.Clientes.DataAccess.Repositories;
using Microservicio.Clientes.DataAccess.Repositories.Interfaces;
using Microservicio.Clientes.DataManagement.Interfaces;
using Microservicio.Clientes.DataManagement.Services;
using Microservicio.Clientes.Business.Interfaces;
using Microservicio.Clientes.Business.Services;

namespace Microservicio.Clientes.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // 🔥 DB CONTEXT
            services.AddDbContext<ClientesDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            // 🔥 REPOSITORIES
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IUsuarioAppRepository, UsuarioAppRepository>();

            // 🔥 UNIT OF WORK
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // 🔥 DATA SERVICES
            services.AddScoped<IClienteDataService, ClienteDataService>();

            // 🔥 BUSINESS SERVICES
            services.AddScoped<IClienteService, ClienteService>();

            return services;
        }
    }
}