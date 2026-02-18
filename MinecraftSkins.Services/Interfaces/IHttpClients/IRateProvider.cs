
using MinecraftSkins.Domain.Enums;

namespace MinecraftSkins.Infrastructure.HttpClients
{
    public interface IRateProvider
    {
        Task<(decimal, RateSource)> GetRateAsync(CancellationToken cancellationToken);
    }
}