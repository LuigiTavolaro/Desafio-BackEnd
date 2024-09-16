using DesafioBackEndProject.Application.DTOs;
using DesafioBackEndProject.Application.Interfaces;
using DesafioBackEndProject.Application.Services;
using DesafioBackEndProject.Domain.Common;
using DesafioBackEndProject.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;

namespace DesafioBackEndProject.Tests
{


    public class MotorcycleServiceTests
    {
        private readonly Mock<IMotorcycleRepository> _motoRepositoryMock;
        private readonly Mock<IValidator<MotorcycleCreateDto>> _validatorMock;
        private readonly Mock<IBus> _busMock;
        private readonly MotorcycleService _motorcycleService;

        private readonly Mock<ILogger<MotorcycleService>> _mockLogger;
        private readonly Mock<NotificationHandler> _mockNotification;

        public MotorcycleServiceTests()
        {
            _motoRepositoryMock = new Mock<IMotorcycleRepository>();
            _validatorMock = new Mock<IValidator<MotorcycleCreateDto>>();
            _busMock = new Mock<IBus>();
            _mockLogger = new Mock<ILogger<MotorcycleService>>();
            _mockNotification = new Mock<NotificationHandler>();
            _motorcycleService = new MotorcycleService(_motoRepositoryMock.Object, _validatorMock.Object, _busMock.Object, _mockLogger.Object, _mockNotification.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfMotorcycles()
        {
            // Arrange
            var motorcycleList = new List<Motorcycle>
        {
            new Motorcycle { Id = 1, Plate = "ABC1234" },
            new Motorcycle { Id = 2, Plate = "XYZ5678" }
        };
            _motoRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(motorcycleList);

            // Act
            var result = await _motorcycleService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _motoRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByPlateAsync_ShouldReturnMotorcycle_WhenExists()
        {
            // Arrange
            var plate = "ABC1234";
            var motorcycle = new Motorcycle { Id = 1, Plate = plate };
            _motoRepositoryMock.Setup(x => x.GetByPlateAsync(plate)).ReturnsAsync(motorcycle);

            // Act
            var result = await _motorcycleService.GetByPlateAsync(plate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(plate, result?.Plate);
            _motoRepositoryMock.Verify(x => x.GetByPlateAsync(plate), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ShouldAddMotorcycle_WhenValid()
        {
            // Arrange
            var motorcycleDto = new MotorcycleCreateDto { Plate = "ABC1234" };
            var validationResult = new FluentValidation.Results.ValidationResult();
            _validatorMock.Setup(x => x.ValidateAsync(motorcycleDto, default)).ReturnsAsync(validationResult);
            _motoRepositoryMock.Setup(x => x.GetByPlateAsync(motorcycleDto.Plate)).ReturnsAsync((Motorcycle)null);
            _motoRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Motorcycle>())).ReturnsAsync(1);

            // Act
            var result = await _motorcycleService.AddAsync(motorcycleDto);

            // Assert
            Assert.True(result > 0);
            _validatorMock.Verify(x => x.ValidateAsync(motorcycleDto, default), Times.Once);
            _motoRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Motorcycle>()), Times.Once);
            _busMock.Verify(x => x.Publish(motorcycleDto, default), Times.Once);
        }

        

        [Fact]
        public async Task DeleteAsync_ShouldCallDelete_WhenNoRentalsExist()
        {
            // Arrange
            var motorcycle = new Motorcycle { Id = 1, Rentals = null };
            _motoRepositoryMock.Setup(x => x.GetByIdAsync(motorcycle.Id)).ReturnsAsync(motorcycle);
            _motoRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);
            // Act
            await _motorcycleService.DeleteAsync(motorcycle.Id);

            // Assert
            _motoRepositoryMock.Verify(x => x.DeleteAsync(motorcycle.Id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldNotCallDelete_WhenRentalsExist()
        {
            // Arrange
            var motorcycle = new Motorcycle { Id = 1, Rentals = new List<Rental> { new Rental() } };
            _motoRepositoryMock.Setup(x => x.GetByIdAsync(motorcycle.Id)).ReturnsAsync(motorcycle);

            // Act
            await _motorcycleService.DeleteAsync(motorcycle.Id);

            // Assert
            _motoRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Never);
        }
    }
}
