using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioBackEndProject.Domain.Entities
{
    [Table("price_range")]
    public record PriceRange
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("max_days")]
        public int MaxDays { get; set; }

        [Column("price_per_day")]
        public decimal PricePerDay { get; set; }
    }
}
