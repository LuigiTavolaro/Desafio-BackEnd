using DesafioBackEndProject.Application.DTOs;
using DesafioBackEndProject.Application.Interfaces;
using DesafioBackEndProject.Domain.Common;
using DesafioBackEndProject.Domain.Entities;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.Logging;

namespace DesafioBackEndProject.Application.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IValidator<DriverCreateDto> _createDriverDtoValidator;
        private readonly ILogger<DriverService> _logger;

        private readonly NotificationHandler _notificationHandler;

        public DriverService(IDriverRepository driverRepository,
                            IValidator<DriverCreateDto> createDriverDtoValidator,
                            ILogger<DriverService> logger,
                            NotificationHandler notificationHandler)
        {
            _driverRepository = driverRepository;
            _createDriverDtoValidator = createDriverDtoValidator;
            _logger = logger;
            _notificationHandler = notificationHandler;
        }

        public async Task<int?> AddAsync(DriverCreateDto driverDto)
        {
            try
            {
                var validationResult = await _createDriverDtoValidator.ValidateAsync(driverDto);
                if (!validationResult.IsValid)
                {
                    validationResult.Errors.ForEach(error =>
                        _notificationHandler.AddNotification(new Notification(error.ErrorMessage, error.PropertyName)));
                    return null;
                }
                var driver = driverDto.Adapt<Driver>();

                var returnDriver = await _driverRepository.GetByCompositeKey(driver).ConfigureAwait(false);

                if (returnDriver)
                {
                    _notificationHandler.AddNotification(new Notification("Entregador já cadastrado"));
                    return null;
                }

                int id = await _driverRepository.AddAsync(driver).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(driverDto.ImagemCnh))
                {
                    await SaveBase64StringAsPng(driverDto.ImagemCnh, id);
                }

                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gravar o entregador");
                _notificationHandler.AddNotification(new Notification("Erro ao cadastrar entregador"));
                return null;
            }
        }

        public async Task UpdateCnhPictureAsync(int id, string newCnh)
        {
            await SaveBase64StringAsPng(newCnh, id);
        }


        private async Task SaveBase64StringAsPng(string base64String, int id)
        {
            try
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
            catch (Exception ex)
            {

                _logger.LogError(ex, "Erro ao salvar o arquivo");
                throw;
            }
        }

    }
}
