using DesafioBackEndProject.Application.DTOs;
using DesafioBackEndProject.Application.Interfaces;
using DesafioBackEndProject.Domain.Entities;
using FluentValidation;
using Mapster;
using MassTransit;

namespace DesafioBackEndProject.Application.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly IMotorcycleRepository _motoRepository;
        private readonly IValidator<MotorcycleCreateDto> _createMotorcycleDtoValidator;
        private readonly IBus _bus;

        public MotorcycleService(IMotorcycleRepository motoRepository, IValidator<MotorcycleCreateDto> createMotorcycleDtoValidator, IBus bus)
        {
            _motoRepository = motoRepository;
            _createMotorcycleDtoValidator = createMotorcycleDtoValidator;
            _bus = bus;
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

        public async Task<int> AddAsync(MotorcycleCreateDto motoDto)
        {
            var validationResult = await _createMotorcycleDtoValidator.ValidateAsync(motoDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }


            var moto = motoDto.Adapt<Motorcycle>();

            var motorcycleByPlate = await _motoRepository.GetByPlateAsync(moto.Plate ?? string.Empty).ConfigureAwait(false);

            if (motorcycleByPlate != null)
                return 0;

            await _bus.Publish(motoDto);

            return await _motoRepository.AddAsync(moto);
        }

        public async Task UpdatePlateAsync(int id, string newPlate)
        {
            await _motoRepository.UpdatePlateAsync(id, newPlate).ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id)
        {
            var moto = await _motoRepository.GetByIdAsync(id).ConfigureAwait(false);

            if (moto?.Rentals?.Any() == true)
            {
                return;
            }

            await _motoRepository.DeleteAsync(id).ConfigureAwait(false);
        }
    }
}
