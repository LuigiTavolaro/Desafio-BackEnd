﻿namespace DesafioBackEndProject.Application.DTOs
{
    public record RentalCreateDto
    {
        public int EntregadorId { get; set; }
        public int MotoId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public DateTime DataPrevisaoTermino { get; set; }
        public int Plano { get; set; }
    }
}



