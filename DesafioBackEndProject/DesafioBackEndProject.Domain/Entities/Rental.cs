using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DesafioBackEndProject.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    [Table("rentals")]
    public record Rental
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("motorcycle_id")]
        public int MotorcycleId { get; set; }
        
        [Required]
        [Column("price_range_id")]
        public int PriceRangeId { get; set; }

        [Required]
        [Column("driver_id")]
        public int DriverId { get; set; }

        [Required]
        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Column("expected_end_date")]
        public DateTime ExpectedEndDate { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public PriceRange? PriceRange { get; set; }

        public Motorcycle? Motorcycle { get; set; }
        public Driver? Driver { get; set; }


    }
}
