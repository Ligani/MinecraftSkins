using MinecraftSkins.Domain.Enums;

namespace MinecraftSkins.Services.Logics
{
    public interface IExchangeRateService
    {
        Task<decimal> GetRateAsync(CancellationToken cancellationToken);
    }
}