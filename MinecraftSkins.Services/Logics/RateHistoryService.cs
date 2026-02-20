using MinecraftSkins.Domain.Models;
using MinecraftSkins.Services.Interfaces.IRepositories;
using MinecraftSkins.Services.Interfaces.IServices;

namespace MinecraftSkins.Services.Logics
{
    public class RateHistoryService : IRateHistoryService
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;

        public RateHistoryService(IExchangeRateRepository exchangeRateRepository)
        {
            _exchangeRateRepository = exchangeRateRepository;
        }

        public async Task<ExchangeRate?> GetLastRateAsync(CancellationToken cancellationToken)
        {
            return await _exchangeRateRepository.GetLastRateAsync(cancellationToken);
        }

        public async Task AddRateAsync(decimal rate, CancellationToken cancellationToken)
        {
            var exchangeRate = new ExchangeRate(rate);

            await _exchangeRateRepository.AddRateAsync(exchangeRate, cancellationToken);
        }
    }

}
