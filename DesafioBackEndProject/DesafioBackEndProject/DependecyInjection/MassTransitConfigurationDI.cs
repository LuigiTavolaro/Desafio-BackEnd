using DesafioBackEndProject.Infrastructure.Messaging;
using MassTransit;

namespace DesafioBackEndProject.DependecyInjection
{
    public static class MassTransitConfigurationDI
    {
        public static void AddMassTransitConfiguration(this IServiceCollection services)
        {

            services.AddMassTransit(x =>
            {
                x.AddConsumer<MotorcycleConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri("rabbitmq://localhost:5672"), h =>
                    {
                        h.Username("guest");  // Credenciais definidas no docker-compose.yml
                        h.Password("guest");
                    });
                    cfg.ReceiveEndpoint("motorcycle-queue", e =>
                    {
                        e.ConfigureConsumer<MotorcycleConsumer>(context);
                    });

                });
            });
        }
    }
}
