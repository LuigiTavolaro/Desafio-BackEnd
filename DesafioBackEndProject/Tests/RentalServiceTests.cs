

using DesafioBackEndProject.Application.DTOs;
using DesafioBackEndProject.Application.Interfaces;
using DesafioBackEndProject.Application.Services;
using DesafioBackEndProject.Domain.Common;
using DesafioBackEndProject.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Microsoft.Extensions.Logging;
using Moq;


namespace DesafioBackEndProject.Tests
{

    public class RentalServiceTests
    {
        private readonly Mock<IRentalRepository> _mockRentalRepository;
        private readonly Mock<IDriverRepository> _mockDriverRepository;
        private readonly Mock<IValidator<RentalCreateDto>> _mockValidator;
        private readonly Mock<IPriceRangeService> _mockPriceRangeService;
        private readonly RentalService _rentalService;

        private readonly Mock<ILogger<RentalService>> _mockLogger;
        private readonly Mock<NotificationHandler> _mockNotification;

        public RentalServiceTests()
        {
            _mockRentalRepository = new Mock<IRentalRepository>();
            _mockDriverRepository = new Mock<IDriverRepository>();
            _mockValidator = new Mock<IValidator<RentalCreateDto>>();
            _mockPriceRangeService = new Mock<IPriceRangeService>();
            _mockLogger = new Mock<ILogger<RentalService>>();
            _mockNotification = new Mock<NotificationHandler>();

            _rentalService = new RentalService(
                _mockRentalRepository.Object,
                _mockValidator.Object,
                _mockDriverRepository.Object,
                _mockPriceRangeService.Object,
                _mockLogger.Object,
                _mockNotification.Object
            );
        }


        [Fact]
        public async Task AddAsync_ShouldReturnNull_WhenDriverDoesNotHaveLicenseTypeA()
        {
            // Arrange
            var rentalDto = new RentalCreateDto { EntregadorId = 1 };
            var validationResult = new ValidationResult();
            _mockValidator.Setup(v => v.ValidateAsync(rentalDto, default))
                          .ReturnsAsync(validationResult);

            var driver = new Driver { Id = 1, DriverLicenseType = "B" }; // Driver sem a licença tipo 'A'
            _mockDriverRepository.Setup(repo => repo.GetById(rentalDto.EntregadorId)).ReturnsAsync(driver);

            // Act
            var result = await _rentalService.AddAsync(rentalDto);

            // Assert
            Assert.Null(result); // Deve retornar 0, pois o driver não tem licença 'A'
            _mockRentalRepository.Verify(repo => repo.AddAsync(It.IsAny<Rental>()), Times.Never);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnRentalId_WhenRentalIsAddedSuccessfully()
        {
            // Arrange
            var rentalDto = new RentalCreateDto { EntregadorId = 1 };
            var validationResult = new ValidationResult();
            _mockValidator.Setup(v => v.ValidateAsync(rentalDto, default)).ReturnsAsync(validationResult);

            var driver = new Driver { Id = 1, DriverLicenseType = "A" }; // Driver com licença 'A'
            _mockDriverRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(driver);


            _mockRentalRepository.Setup(repo => repo.AddAsync(It.IsAny<Rental>())).ReturnsAsync(1); // Simulando o retorno do ID

            // Act
            var result = await _rentalService.AddAsync(rentalDto);

            // Assert
            Assert.Equal(1, result); // Deve retornar o ID do rental adicionado
            _mockRentalRepository.Verify(repo => repo.AddAsync(It.IsAny<Rental>()), Times.Once);
        }

        [Fact]
        public async Task CalculateRentalReturnPrice_ShouldReturnCalculatedPrice_WhenRentalExists()
        {
            // Arrange
            var rental = new Rental { Id = 1, StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(-5) };
            _mockRentalRepository.Setup(repo => repo.GetByIdAsync(rental.Id)).ReturnsAsync(rental);

            var returnDate = DateTime.Now;
            _mockPriceRangeService.Setup(service => service.GetPrice(rental.StartDate, rental.EndDate, rental.ExpectedEndDate, returnDate, rental))
                                  .ReturnsAsync(500m); // Simulando o cálculo do preço

            // Act
            var result = await _rentalService.CalculateRentalReturnPrice(rental.Id, returnDate);

            // Assert
            Assert.Equal(500m, result); // Deve retornar o preço calculado
            _mockRentalRepository.Verify(repo => repo.GetByIdAsync(rental.Id), Times.Once);
            _mockPriceRangeService.Verify(service => service.GetPrice(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Rental>()), Times.Once);
        }

        [Fact]
        public async Task CalculateRentalReturnPrice_ShouldReturnZero_WhenRentalDoesNotExist()
        {
            // Arrange
            _mockRentalRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Rental?)null);

            // Act
            var result = await _rentalService.CalculateRentalReturnPrice(1, DateTime.Now);

            // Assert
            Assert.Null(result); // Deve retornar 0, pois o aluguel não existe
            _mockRentalRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Once);
            _mockPriceRangeService.Verify(service => service.GetPrice(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Rental>()), Times.Never);
        }
    }
}
