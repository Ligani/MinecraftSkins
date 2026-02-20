using MinecraftSkins.Domain.Enums;

namespace MinecraftSkins.Services.Interfaces.IHttpClients
{
    public interface IRateProvider
    {
        Task<(decimal Rate, RateSource Source)> GetRateAsync(CancellationToken cancellationToken);
    }
}