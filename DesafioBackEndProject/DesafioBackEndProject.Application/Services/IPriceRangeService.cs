using DesafioBackEndProject.Application.DTOs;

namespace DesafioBackEndProject.Application.Services
{
    public interface IPriceRangeService
    {
        Task<PriceRangeDto> GetPrices();

    }
}
