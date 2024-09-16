using DesafioBackEndProject.Application.Interfaces;
using DesafioBackEndProject.Domain.Entities;
using DesafioBackEndProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackEndProject.Infrastructure.Repositories
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly DesafioDbContext _context;

        public MotorcycleRepository(DesafioDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Motorcycle>> GetAllAsync()
        {
            return await _context.Motos.ToListAsync();
        }

        public async Task<Motorcycle?> GetByIdAsync(int id)
        {
            return await _context.Motos
                .Include(r => r.Rentals)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Motorcycle?> GetByPlateAsync(string plate)
        {
            return await _context.Motos.FirstOrDefaultAsync(x => x.Plate == plate);
        }

        public async Task<int> AddAsync(Motorcycle moto)
        {
            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();
            return moto.Id;
        }

        public async Task UpdatePlateAsync(int id, string newPlate)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto != null)
            {
                moto.Plate = newPlate;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto != null)
            {
                _context.Motos.Remove(moto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
