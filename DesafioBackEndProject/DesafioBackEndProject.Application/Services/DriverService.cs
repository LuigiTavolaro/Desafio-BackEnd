using DesafioBackEndProject.Application.DTOs;
using DesafioBackEndProject.Application.Interfaces;
using DesafioBackEndProject.Domain.Entities;
using FluentValidation;
using Mapster;

namespace DesafioBackEndProject.Application.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IValidator<DriverCreateDto> _createDriverDtoValidator;


        public DriverService(IDriverRepository driverRepository,
                            IValidator<DriverCreateDto> createDriverDtoValidator)
        {
            _driverRepository = driverRepository;
            _createDriverDtoValidator = createDriverDtoValidator;
        }

        public async Task<int?> AddAsync(DriverCreateDto driverDto)
        {
            var validationResult = await _createDriverDtoValidator.ValidateAsync(driverDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var driver = driverDto.Adapt<Driver>();

            var returnDriver = await _driverRepository.GetByCompositeKey(driver).ConfigureAwait(false);

            if (returnDriver) return null;

            int id = await _driverRepository.AddAsync(driver).ConfigureAwait(false);

            if (!string.IsNullOrEmpty(driverDto.ImagemCnh))
            {
                await SaveBase64StringAsPng(driverDto.ImagemCnh, id);
            }

            return id;
        }

        public async Task UpdateCnhPictureAsync(int id, string newCnh)
        {
            await SaveBase64StringAsPng(newCnh, id);
        }


        public static async Task SaveBase64StringAsPng(string base64String, int id)
        {
            if (base64String.Contains(","))
            {
                base64String = base64String.Split(',')[1];
            }

            byte[] imageBytes = Convert.FromBase64String(base64String);


            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string picturesFolder = Path.Combine(baseDirectory, "Pictures");

            if (!Directory.Exists(picturesFolder))
            {
                Directory.CreateDirectory(picturesFolder);
            }

            string fileName = $"image_{id}.png";
            string filePath = Path.Combine(picturesFolder, fileName);


            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }

            await File.WriteAllBytesAsync(filePath, imageBytes);
        }

    }
}
