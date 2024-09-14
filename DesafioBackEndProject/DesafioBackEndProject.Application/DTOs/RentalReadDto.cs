namespace DesafioBackEndProject.Application.DTOs
{
    public record RentalReadDto
    {
        public string? Identificador { get; set; }
        public int ValorDiaria { get; set; }
        public string? EntregadorId { get; set; }
        public string? MotoId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public DateTime DataPrevisaoTermino { get; set; }
        public DateTime DataDevolucao { get; set; }
    }
}




