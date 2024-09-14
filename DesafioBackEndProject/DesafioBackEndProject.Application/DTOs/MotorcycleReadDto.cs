using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEndProject.Application.DTOs
{
    public record MotorcycleReadDto
    {
        public int Id { get; set; }
        public int ManufacturingYear { get; set; }
        public string? Model { get; set; }
        public string? Brand { get; set; }
        public string? Plate { get; set; }
    }
}
