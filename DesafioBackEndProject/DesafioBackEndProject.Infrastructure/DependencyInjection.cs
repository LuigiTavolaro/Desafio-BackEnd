using DesafioBackEndProject.Application.Interfaces;
using DesafioBackEndProject.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioBackEndProject.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            // Registro dos serviços
            services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
        }
    }
}
