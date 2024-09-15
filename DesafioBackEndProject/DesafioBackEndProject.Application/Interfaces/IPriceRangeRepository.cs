using DesafioBackEndProject.Domain.Entities;

namespace DesafioBackEndProject.Application.Interfaces
{
    public interface IPriceRangeRepository
    {
        Task<IEnumerable<PriceRange>> GetPrices();

    }
}
