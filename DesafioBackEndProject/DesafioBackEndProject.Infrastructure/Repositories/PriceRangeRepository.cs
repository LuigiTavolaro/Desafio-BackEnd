using DesafioBackEndProject.Application.Interfaces;
using DesafioBackEndProject.Domain.Entities;
using DesafioBackEndProject.Infrastructure.Cache;
using DesafioBackEndProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackEndProject.Infrastructure.Repositories
{
    public class PriceRangeRepository : IPriceRangeRepository
    {
        private readonly DesafioDbContext _context;

        private const string KEY_CACHE = "prices-range";

        public PriceRangeRepository(DesafioDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PriceRange>> GetPrices()
        {
            var pricesRange = CacheManager.Get<IEnumerable<PriceRange>>(KEY_CACHE);

            if (pricesRange != null) return pricesRange;

            pricesRange = await _context.PricesRange.ToListAsync();

            if (pricesRange != null)
            {
                CacheManager.Set(KEY_CACHE, pricesRange, TimeSpan.FromHours(2));
            }

            return pricesRange ?? new List<PriceRange>();

        }
    }
}
