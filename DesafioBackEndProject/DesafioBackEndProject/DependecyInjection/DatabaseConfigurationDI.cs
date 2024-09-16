using DesafioBackEndProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackEndProject.DependecyInjection
{
    public static class DatabaseConfigurationDI
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, ConfigurationManager configuration )
        {
            services.AddDbContext<DesafioDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    npgsqlOptions => npgsqlOptions
                        .EnableRetryOnFailure() // Configura retry em caso de falha
                        .CommandTimeout(60) // Tempo limite para comandos
                        .MinBatchSize(1) // Configura o tamanho mínimo do lote
                        .MaxBatchSize(10)) // Configura o tamanho máximo do lote
            );
        }
    }
}
