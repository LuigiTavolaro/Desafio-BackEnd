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
        private readonly IValidator<RentalCreateDto> _createRentalDtoValidator;


        public RentalService(IRentalRepository rentalRepository, IValidator<RentalCreateDto> createRentalDtoValidator)
        {
            _rentalRepository = rentalRepository;
            _createRentalDtoValidator = createRentalDtoValidator;
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

            return await _rentalRepository.AddAsync(rental);
        }

    }
}
