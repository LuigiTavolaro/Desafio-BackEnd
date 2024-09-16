using System.Diagnostics.CodeAnalysis;

namespace DesafioBackEndProject.Application.DTOs
{
    [ExcludeFromCodeCoverage]
    public record PriceRangeDto
    {
        public int Id { get; set; }

        public int MaxDays { get; set; }

        public decimal PricePerDay { get; set; }
    }

}