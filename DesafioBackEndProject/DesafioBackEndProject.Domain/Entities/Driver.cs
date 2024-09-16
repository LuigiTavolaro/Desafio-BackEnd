using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DesafioBackEndProject.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    [Table("drivers")]
    public record Driver
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string? Name { get; set; }

        [Required]
        [Column("cnpj")]
        public string? Cnpj { get; set; }

        [Required]
        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Column("driver_license_number")]
        public string? DriverLicenseNumber { get; set; }

        [Required]
        [Column("driver_license_type")]
        public string? DriverLicenseType { get; set; }

        [Column("driver_license_image_url")]
         public string? DriverLicenseImageUrl { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Rental>? Rentals { get; set; }
    }
}
