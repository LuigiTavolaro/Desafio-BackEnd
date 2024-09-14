using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioBackEndProject.Domain.Entities
{
    [Table("motorcycles")]
    public record Motorcycle
    {
        [Column("id")] // Nome da coluna no banco de dados
        public int Id { get; set; }
        [Column("manufacturing_year")]
        public int ManufacturingYear { get; set; }
        [Column("model")]
        public string? Model { get; set; }
        [Column("brand")]
        public string? Brand { get; set; }
        [Column("plate")]
        public string? Plate { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

    }
}
