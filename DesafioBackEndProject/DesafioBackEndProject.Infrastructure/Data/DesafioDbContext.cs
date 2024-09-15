using DesafioBackEndProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackEndProject.Infrastructure.Data
{
    public class DesafioDbContext : DbContext
    {
        public DesafioDbContext(DbContextOptions<DesafioDbContext> options) : base(options) { }

        public DbSet<Motorcycle> Motos { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<PriceRange> PricesRange { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Motorcycle>().HasKey(m => m.Id);
            modelBuilder.Entity<Driver>().HasKey(m => m.Id);
            modelBuilder.Entity<Rental>().HasKey(m => m.Id);
            modelBuilder.Entity<PriceRange>().HasKey(m => m.Id);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Motorcycle)
                .WithMany(m => m.Rentals)
                .HasForeignKey(r => r.MotorcycleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Driver)
                .WithMany(d => d.Rentals)
                .HasForeignKey(r => r.DriverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
