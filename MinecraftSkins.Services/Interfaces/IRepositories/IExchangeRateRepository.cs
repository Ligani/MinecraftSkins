using MinecraftSkins.Domain.Models;

namespace MinecraftSkins.Infrastructure.Repositories
{
    public interface IExchangeRateRepository
    {
        Task AddRateAsync(ExchangeRate rate, CancellationToken cancellationToken);
        Task<ExchangeRate?> GetLastRateAsync(CancellationToken cancellationToken);
    }
}