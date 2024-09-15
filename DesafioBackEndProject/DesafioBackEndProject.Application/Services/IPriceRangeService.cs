using DesafioBackEndProject.Application.DTOs;
using DesafioBackEndProject.Domain.Entities;

namespace DesafioBackEndProject.Application.Services
{
    public interface IPriceRangeService
    {
        Task<decimal> GetPrice(DateTime startDate, DateTime endDate, DateTime returnDate, Rental plan);
    }
}
