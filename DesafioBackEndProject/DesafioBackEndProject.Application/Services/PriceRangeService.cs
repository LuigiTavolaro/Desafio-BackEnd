using DesafioBackEndProject.Application.Interfaces;
using DesafioBackEndProject.Domain.Entities;

namespace DesafioBackEndProject.Application.Services
{
    public class PriceRangeService : IPriceRangeService
    {
        private readonly IPriceRangeRepository _priceRangeRepository;

        private const decimal ADDITIONAL_DAY_CHARGE = 50.00m;

        public PriceRangeService(IPriceRangeRepository priceRangeRepository)
        {
            _priceRangeRepository = priceRangeRepository;
        }

        public async Task<decimal> GetPrice(DateTime startDate, DateTime endDate, DateTime expectedEndDate, DateTime returnDate, Rental plan)
        {
            var priceRange = await _priceRangeRepository.GetPrices();


            if (returnDate < expectedEndDate)
            {
                throw new ArgumentException("A data de devolução não pode ser antes da data de término prevista.");
            }

            if (!priceRange.Any(o => o.Id == plan.PriceRangeId))
            {
                throw new ArgumentException("Plano de locação inválido.");
            }

            // Calcula o período de locação
            var rentalPeriod = (endDate - startDate).Days;
            var dailyRate = priceRange.FirstOrDefault(o => o.Id == plan.PriceRangeId);

            // Calcula o valor total inicial da locação
            decimal initialCost = rentalPeriod * dailyRate.PricePerDay;

            // Verifica se a devolução foi feita após o término
            if (returnDate > endDate)
            {
                var additionalDays = (returnDate - endDate).Days;
                return initialCost + (additionalDays * ADDITIONAL_DAY_CHARGE);
            }

            // Calcula a multa para locações antes do término previsto
            var unusedDays = (endDate - returnDate).Days;
            var penaltyRate = dailyRate.PenaltyRate ?? 1;
            decimal penalty = unusedDays * dailyRate.PricePerDay * (1 + penaltyRate / 100);

            return initialCost - penalty;

        }
    }
}
