namespace DesafioBackEndProject.Application.DTOs
{
    public record PriceRangeDto
    {
        public int Id { get; set; }

        public int MaxDays { get; set; }

        public decimal PricePerDay { get; set; }
    }

}