using DesafioBackEndProject.Application.DTOs;
using DesafioBackEndProject.Application.Interfaces;
using DesafioBackEndProject.Domain.Common;
using DesafioBackEndProject.Domain.Entities;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.Logging;

namespace DesafioBackEndProject.Application.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IValidator<RentalCreateDto> _createRentalDtoValidator;
        private readonly ILogger<RentalService> _logger;
        private readonly IPriceRangeService _priceRangeService;
        private readonly NotificationHandler _notificationHandler;

        public RentalService(IRentalRepository rentalRepository, IValidator<RentalCreateDto> createRentalDtoValidator, IDriverRepository driverRepository, IPriceRangeService priceRangeService, ILogger<RentalService> logger, NotificationHandler notificationHandler)
        {
            _rentalRepository = rentalRepository;
            _createRentalDtoValidator = createRentalDtoValidator;
            _driverRepository = driverRepository;
            _priceRangeService = priceRangeService;
            _logger = logger;
            _notificationHandler = notificationHandler;
        }


        public async Task<RentalReadDto?> GetByIdAsync(int id)
        {
            var rental = await _rentalRepository.GetByIdAsync(id).ConfigureAwait(false);

            return rental.Adapt<RentalReadDto?>();
        }

        public async Task<int?> AddAsync(RentalCreateDto rentalDto)
        {
            try
            {
                var validationResult = await _createRentalDtoValidator.ValidateAsync(rentalDto);
                if (!validationResult.IsValid)
                {
                    validationResult.Errors.ForEach(error =>
                        _notificationHandler.AddNotification(new Notification(error.ErrorMessage, error.PropertyName)));
                }

                var rental = rentalDto.Adapt<Rental>();

                var driver = await _driverRepository.GetById(rental.DriverId).ConfigureAwait(false);

                if (driver?.DriverLicenseType?.Contains('A') != true)
                {
                    _notificationHandler.AddNotification(new Notification("Entregador não tem carteira habilitada A ou A+B"));
                    return null;
                }


                return await _rentalRepository.AddAsync(rental).ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar uma locação");
                _notificationHandler.AddNotification(new Notification("Erro ao cadastrar uma locação"));
                return null;
            }
        }

        public async Task<decimal?> CalculateRentalReturnPrice(int id, DateTime dataDevoluacao)
        {
            var rental = await _rentalRepository.GetByIdAsync(id).ConfigureAwait(false);

            if (rental == null)
            {
                _notificationHandler.AddNotification(new Notification("Locação não cadastrada"));

                return null;
            }

            var calculatePrice = await _priceRangeService.GetPrice(rental.StartDate, rental.EndDate, dataDevoluacao, rental).ConfigureAwait(false);

            return calculatePrice;

        }
    }
}
