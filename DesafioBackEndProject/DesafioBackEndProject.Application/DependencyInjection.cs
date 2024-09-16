using DesafioBackEndProject.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioBackEndProject.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Registro dos serviços
            services.AddScoped<IMotorcycleService, MotorcycleService>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<IRentalService, RentalService>();
            services.AddScoped<IPriceRangeService, PriceRangeService>();
        }
    }
}
