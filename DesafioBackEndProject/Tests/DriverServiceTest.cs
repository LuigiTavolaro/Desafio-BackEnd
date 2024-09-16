
using DesafioBackEndProject.Application.DTOs;
using DesafioBackEndProject.Application.Interfaces;
using DesafioBackEndProject.Application.Services;
using DesafioBackEndProject.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Moq;


namespace DesafioBackEndProject.Tests
{
    public class DriverServiceTests
    {
        private readonly Mock<IDriverRepository> _mockDriverRepository;
        private readonly Mock<IValidator<DriverCreateDto>> _mockValidator;
        private readonly DriverService _driverService;

        public DriverServiceTests()
        {
            _mockDriverRepository = new Mock<IDriverRepository>();
            _mockValidator = new Mock<IValidator<DriverCreateDto>>();
            _driverService = new DriverService(_mockDriverRepository.Object, _mockValidator.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnNull_WhenDriverAlreadyExists()
        {
            // Arrange
            var driverCreateDto = new DriverCreateDto { Nome = "John Doe", NumeroCnh = "123456" };

            var validationResult = new ValidationResult();
            _mockValidator.Setup(v => v.ValidateAsync(driverCreateDto, default))
                          .ReturnsAsync(validationResult);

            
            _mockDriverRepository.Setup(repo => repo.GetByCompositeKey(It.IsAny<Driver>()))
                                 .ReturnsAsync(true); // Simula que o driver já existe

            // Act
            var result = await _driverService.AddAsync(driverCreateDto);

            // Assert
            Assert.Null(result); // Deve retornar null, já que o driver existe
            _mockDriverRepository.Verify(repo => repo.AddAsync(It.IsAny<Driver>()), Times.Never);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowValidationException_WhenValidationFails()
        {
            // Arrange
            var driverCreateDto = new DriverCreateDto { Nome = "John Doe", NumeroCnh = "123456" };

            var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name is required")
        });

            _mockValidator.Setup(v => v.ValidateAsync(driverCreateDto, default))
                          .ReturnsAsync(validationResult); // Simula falha na validação

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _driverService.AddAsync(driverCreateDto));

            Assert.Equal("Validation failed: \r\n -- Name: Name is required Severity: Error", exception.Message);
            _mockDriverRepository.Verify(repo => repo.AddAsync(It.IsAny<Driver>()), Times.Never);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnDriverId_WhenDriverIsAddedSuccessfully()
        {
            // Arrange
            var driverCreateDto = new DriverCreateDto
            {
                Nome = "John Doe",
                NumeroCnh = "123456"
            };

            var validationResult = new ValidationResult();
            _mockValidator.Setup(v => v.ValidateAsync(driverCreateDto, default))
                          .ReturnsAsync(validationResult);

            _mockDriverRepository.Setup(repo => repo.GetByCompositeKey(It.IsAny<Driver>()))
                                 .ReturnsAsync(false); // Simula que o driver não existe
            _mockDriverRepository.Setup(repo => repo.AddAsync(It.IsAny<Driver>()))
                                 .ReturnsAsync(1); // Simula a adição do driver e o retorno do Id

            // Act
            var result = await _driverService.AddAsync(driverCreateDto);

            // Assert
            Assert.Equal(1, result); // O Id do motorista adicionado deve ser 1
            _mockDriverRepository.Verify(repo => repo.AddAsync(It.IsAny<Driver>()), Times.Once);
        }

    }

}


