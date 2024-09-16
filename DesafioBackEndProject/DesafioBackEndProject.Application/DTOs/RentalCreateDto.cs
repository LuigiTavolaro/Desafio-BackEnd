using System.Diagnostics.CodeAnalysis;

namespace DesafioBackEndProject.Application.DTOs
{
    [ExcludeFromCodeCoverage]
    public record RentalCreateDto
    {
        public int EntregadorId { get; set; }
        public int MotoId { get; set; }
        public int PlanoId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public DateTime DataPrevisaoTermino { get; set; }
        
    }
}



