using DesafioBackEndProject.Application.DTOs;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace DesafioBackEndProject.Infrastructure.Messaging
{
    public class MotorcycleConsumer : IConsumer<MotorcycleCreateDto>
    {
        private readonly ILogger<MotorcycleConsumer> _logger;

        public MotorcycleConsumer(ILogger<MotorcycleConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<MotorcycleCreateDto> context)
        {
            if (context == null) throw new ArgumentNullException("context");

            if (context.Message.ManufacturingYear.Equals(2024))
            {
                _logger.LogInformation("notificado");
            }

            return Task.CompletedTask;
        }
    }
}
