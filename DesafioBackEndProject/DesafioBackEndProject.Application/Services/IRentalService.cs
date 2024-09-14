using DesafioBackEndProject.Application.DTOs;

namespace DesafioBackEndProject.Application.Services
{
    public interface IRentalService
    {
        Task<RentalReadDto?> GetByIdAsync(int id);

        Task<int> AddAsync(RentalCreateDto moto);
    }
}
