using DesafioBackEndProject.Application.Interfaces;
using DesafioBackEndProject.Domain.Entities;
using DesafioBackEndProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackEndProject.Infrastructure.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly DesafioDbContext _context;

        public DriverRepository(DesafioDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Driver driver)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();
            return driver.Id;
        }

        public async Task<bool> GetByCompositeKey(Driver driver)
        {
           return await _context.Drivers.AnyAsync(m => m.DriverLicenseNumber == driver.DriverLicenseNumber || m.Cnpj == driver.Cnpj);
           
        }

        public Task UpdateCnhPictureAsync(int id, string newCnh)
        {
            throw new NotImplementedException();
        }
    }
}
