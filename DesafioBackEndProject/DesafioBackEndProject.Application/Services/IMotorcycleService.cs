using DesafioBackEndProject.Application.DTOs;

namespace DesafioBackEndProject.Application.Services
{
    public interface IMotorcycleService
    {
        Task<IEnumerable<MotorcycleReadDto>> GetAllAsync();
        Task<MotorcycleReadDto?> GetByIdAsync(int id);
        Task<MotorcycleReadDto?> GetByPlateAsync(string plate);
        Task<int?> AddAsync(MotorcycleCreateDto moto);
        Task UpdatePlateAsync(int id, string newPlate);
        Task DeleteAsync(int id);
    }
}
