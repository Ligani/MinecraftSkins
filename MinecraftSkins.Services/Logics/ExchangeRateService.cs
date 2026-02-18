using MinecraftSkins.Domain.Enums;
using MinecraftSkins.Infrastructure.HttpClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftSkins.Services.Logics
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IRateProvider _rateProvider;

        public ExchangeRateService(IRateProvider rateProvider)
        {
            _rateProvider = rateProvider;
        }

        public async Task<decimal> GetRateAsync(CancellationToken cancellationToken)
        {
            (decimal Rate, RateSource Source) = await _rateProvider.GetRateAsync(cancellationToken);
            return Rate;
        }
    }
}
