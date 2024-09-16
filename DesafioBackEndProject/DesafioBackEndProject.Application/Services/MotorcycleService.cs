using DesafioBackEndProject.Application.DTOs;
using DesafioBackEndProject.Application.Interfaces;
using DesafioBackEndProject.Domain.Common;
using DesafioBackEndProject.Domain.Entities;
using FluentValidation;
using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace DesafioBackEndProject.Application.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly IMotorcycleRepository _motoRepository;
        private readonly IValidator<MotorcycleCreateDto> _createMotorcycleDtoValidator;
        private readonly IBus _bus;
        private readonly ILogger<MotorcycleService> _logger;

        private readonly NotificationHandler _notificationHandler;

        public MotorcycleService(IMotorcycleRepository motoRepository, IValidator<MotorcycleCreateDto> createMotorcycleDtoValidator, IBus bus, ILogger<MotorcycleService> logger, NotificationHandler notificationHandler)
        {
            _motoRepository = motoRepository;
            _createMotorcycleDtoValidator = createMotorcycleDtoValidator;
            _bus = bus;
            _logger = logger;
            _notificationHandler = notificationHandler;
        }

        public async Task<IEnumerable<MotorcycleReadDto>> GetAllAsync()
        {
            var motos = await _motoRepository.GetAllAsync().ConfigureAwait(false);
            // Usa o Mapster para mapear a lista de motocicletas para DTOs
            return motos.Adapt<IEnumerable<MotorcycleReadDto>>();
        }

        public async Task<MotorcycleReadDto?> GetByPlateAsync(string plate)
        {
            var moto = await _motoRepository.GetByPlateAsync(plate).ConfigureAwait(false);
            // Usa o Mapster para mapear a motocicleta para DTO
            return moto.Adapt<MotorcycleReadDto?>();
        }
        public async Task<MotorcycleReadDto?> GetByIdAsync(int id)
        {
            var moto = await _motoRepository.GetByIdAsync(id).ConfigureAwait(false);
            return moto.Adapt<MotorcycleReadDto?>();
        }

        public async Task<int?> AddAsync(MotorcycleCreateDto motoDto)
        {
            try
            {
                var validationResult = await _createMotorcycleDtoValidator.ValidateAsync(motoDto);
                if (!validationResult.IsValid)
                {
                    validationResult.Errors.ForEach(error =>
                        _notificationHandler.AddNotification(new Notification(error.ErrorMessage, error.PropertyName)));
                    return null;
                }


                var moto = motoDto.Adapt<Motorcycle>();

                var motorcycleByPlate = await _motoRepository.GetByPlateAsync(moto.Plate ?? string.Empty).ConfigureAwait(false);

                if (motorcycleByPlate != null)
                {
                    _notificationHandler.AddNotification(new Notification("Placa já cadastrada"));
                    return null;
                }

                await _bus.Publish(motoDto);

                return await _motoRepository.AddAsync(moto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar uma motocicleta");
                _notificationHandler.AddNotification(new Notification("Erro ao cadastrar uma motocicleta"));
                return null;
            }
        }

        public async Task UpdatePlateAsync(int id, string newPlate)
        {
            try
            {
                await _motoRepository.UpdatePlateAsync(id, newPlate).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar a motocicleta");
                _notificationHandler.AddNotification(new Notification("Erro ao atualizar uma motocicleta")); 
                return;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {

                var moto = await _motoRepository.GetByIdAsync(id).ConfigureAwait(false);

                if (moto?.Rentals?.Any() == true)
                {
                    _notificationHandler.AddNotification(new Notification("Não foi possível apagar o registro, pois existem locações cadastradas para essa motocicleta"));
                    return;
                }

                await _motoRepository.DeleteAsync(id).ConfigureAwait(false);

            }
            catch (Exception ex )
            {
                _logger.LogError(ex, "Erro ao deletar a matocicleta");
                _notificationHandler.AddNotification(new Notification("Erro ao deletar a matocicleta"));
                return;
            }
        }
    }
}
