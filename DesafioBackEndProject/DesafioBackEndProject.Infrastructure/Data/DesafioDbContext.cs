using DesafioBackEndProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackEndProject.Infrastructure.Data
{
    public class DesafioDbContext : DbContext
    {
        public DesafioDbContext(DbContextOptions<DesafioDbContext> options) : base(options) { }

        public DbSet<Motorcycle> Motos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Motorcycle>().HasKey(m => m.Id);
        }
    }
}
