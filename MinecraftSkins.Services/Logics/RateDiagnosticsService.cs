using MinecraftSkins.Services.Interfaces.IHttpClients;
using MinecraftSkins.Services.Interfaces.IServices;
using MinecraftSkins__BTC_indexed_pricing_.DTO;

namespace MinecraftSkins.Services.Logics
{
    public class RateDiagnosticsService : IRateDiagnosticsService
    {
        private readonly IRateProvider _btcProvider;

        public RateDiagnosticsService(IRateProvider btcProvider)
        {
            _btcProvider = btcProvider;
        }

        public async Task<RateDiagnosticsResponse> GetDiagnosticsAsync(CancellationToken cancellationToken)
        {
            var (rate, source) = await _btcProvider.GetRateAsync(cancellationToken);

            return new RateDiagnosticsResponse(rate, source.ToString(), DateTime.UtcNow);
        }
    }

}
