using DesafioBackEndProject.Application.DTOs;
using DesafioBackEndProject.Application.Interfaces;
using DesafioBackEndProject.Domain.Entities;
using FluentValidation;
using Mapster;

namespace DesafioBackEndProject.Application.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IValidator<RentalCreateDto> _createRentalDtoValidator;

        private readonly IPriceRangeService _priceRangeService;

        public RentalService(IRentalRepository rentalRepository, IValidator<RentalCreateDto> createRentalDtoValidator, IDriverRepository driverRepository, IPriceRangeService priceRangeService)
        {
            _rentalRepository = rentalRepository;
            _createRentalDtoValidator = createRentalDtoValidator;
            _driverRepository = driverRepository;
            _priceRangeService = priceRangeService;
        }


        public async Task<RentalReadDto?> GetByIdAsync(int id)
        {
            var rental = await _rentalRepository.GetByIdAsync(id).ConfigureAwait(false);

            return rental.Adapt<RentalReadDto?>();
        }

        public async Task<int> AddAsync(RentalCreateDto rentalDto)
        {
            var validationResult = await _createRentalDtoValidator.ValidateAsync(rentalDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var rental = rentalDto.Adapt<Rental>();

            var driver = await _driverRepository.GetById(rental.DriverId).ConfigureAwait(false);

            if (driver?.DriverLicenseType?.Contains('A') != true)
            {
                return 0;
            }


            return await _rentalRepository.AddAsync(rental);
        }

        public async Task<decimal> CalculateRentalReturnPrice(int id, DateTime dataDevoluacao)
        {
            var rental = await _rentalRepository.GetByIdAsync(id).ConfigureAwait(false);

            if(rental == null) return 0;

            var calculatePrice = await _priceRangeService.GetPrice(rental.StartDate, rental.EndDate, dataDevoluacao, rental).ConfigureAwait(false);

            return calculatePrice;

        }
    }
}
