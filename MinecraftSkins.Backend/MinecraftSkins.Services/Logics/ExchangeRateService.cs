using MinecraftSkins.Domain.Enums;
using MinecraftSkins.Services.Interfaces.IHttpClients;
using MinecraftSkins.Services.Interfaces.IServices;


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
