using System.Diagnostics.CodeAnalysis;

namespace DesafioBackEndProject.Application.DTOs
{
    [ExcludeFromCodeCoverage]
    public record MotorcycleReadDto
    {
        public int Id { get; set; }
        public int ManufacturingYear { get; set; }
        public string? Model { get; set; }
        public string? Brand { get; set; }
        public string? Plate { get; set; }
    }
}
