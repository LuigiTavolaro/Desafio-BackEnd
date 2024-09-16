using DesafioBackEndProject.Application.DTOs;
using DesafioBackEndProject.Domain.Entities;
using Mapster;

namespace DesafioBackEndProject.Infrastructure.Mapping
{
    public class MappingConfig
    {
        public static void ConfigureMappings()
        {
            TypeAdapterConfig<DriverCreateDto, Driver>
                .NewConfig()
                .Map(dest => dest.DriverLicenseType, src => src.TipoCnh)
                .Map(dest => dest.BirthDate, src => src.DataNascimento)
                .Map(dest => dest.Cnpj, src => src.Cnpj)
                .Map(dest => dest.DriverLicenseNumber, src => src.NumeroCnh)
                .Map(dest => dest.Name, src => src.Nome)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.UpdatedAt)
                .Ignore(dest => dest.DriverLicenseImageUrl);

            TypeAdapterConfig<RentalCreateDto, Rental>
                .NewConfig()
                .Map(dest => dest.MotorcycleId, src => src.MotoId)
                .Map(dest => dest.DriverId, src => src.EntregadorId)
                .Map(dest => dest.PriceRangeId, src => src.PlanoId)
                .Map(dest => dest.EndDate, src => src.DataTermino)
                .Map(dest => dest.StartDate, src => src.DataInicio)
                .Map(dest => dest.ExpectedEndDate, src => src.DataPrevisaoTermino)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.UpdatedAt)
                .Ignore(dest => dest.Motorcycle)
                .Ignore(dest => dest.Driver)
                .Ignore(dest => dest.Id);


            TypeAdapterConfig<Rental, RentalReadDto>
                .NewConfig()
                .Map(dest => dest.DataPrevisaoTermino, src => src.ExpectedEndDate)
                .Map(dest => dest.EntregadorId, src => src.DriverId)
                .Map(dest => dest.MotoId, src => src.MotorcycleId)
                .Map(dest => dest.PlanoId, src => src.PriceRangeId)
                .Map(dest => dest.DataTermino, src => src.EndDate)
                .Map(dest => dest.DataInicio, src => src.StartDate)
                .Map(dest => dest.DataPrevisaoTermino, src => src.ExpectedEndDate);
        }
    }
}
