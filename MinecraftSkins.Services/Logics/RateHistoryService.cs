using MinecraftSkins.Domain.Models;
using MinecraftSkins.Infrastructure.Repositories;
using MinecraftSkins.Services.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
