using DesafioBackEndProject.Application.Interfaces;
using DesafioBackEndProject.Domain.Entities;
using DesafioBackEndProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackEndProject.Infrastructure.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly DesafioDbContext _context;

        public RentalRepository(DesafioDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Rental rental)
        {
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
            return rental.Id;
        }

        public async Task<Rental?> GetByIdAsync(int id)
        {
            return await _context.Rentals
            .Include(r => r.Motorcycle) 
            .Include(r => r.Driver)
            .Include(r => r.PriceRange)
            .FirstOrDefaultAsync(r => r.Id == id); 
        }
    }
}
