using DesafioBackEndProject.Application.DTOs;
using DesafioBackEndProject.Application.Interfaces;
using DesafioBackEndProject.Domain.Entities;
using FluentValidation;
using Mapster;

namespace DesafioBackEndProject.Application.Services
{
    public class PriceRangeService : IPriceRangeService
    {
        private readonly IPriceRangeRepository _priceRangeRepository;
       

        public PriceRangeService()
        {

        }

        public Task<PriceRangeDto> GetPrices()
        {
            throw new NotImplementedException();
        }
    }
}
