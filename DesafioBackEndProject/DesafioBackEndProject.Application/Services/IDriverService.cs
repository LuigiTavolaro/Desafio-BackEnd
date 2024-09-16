using DesafioBackEndProject.Application.DTOs;

namespace DesafioBackEndProject.Application.Services
{
    public interface IDriverService
    {
        Task<int?> AddAsync(DriverCreateDto driver);
        Task UpdateCnhPictureAsync(int id, string newCnh);
    }
}
