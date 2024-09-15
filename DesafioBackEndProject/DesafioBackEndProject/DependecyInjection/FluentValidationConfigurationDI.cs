using DesafioBackEndProject.Application.Validators;
using FluentValidation;

namespace DesafioBackEndProject.DependecyInjection
{
    public static class FluentValidationConfigurationDI
    {
        public static void AddFluentValidationConfiguration(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<MotorcycleValidator>();
            services.AddValidatorsFromAssemblyContaining<DriverValidator>();
            services.AddValidatorsFromAssemblyContaining<RentalValidator>();
        }
    }
}
