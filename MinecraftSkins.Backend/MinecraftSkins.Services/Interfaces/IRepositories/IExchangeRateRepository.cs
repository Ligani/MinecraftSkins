using MinecraftSkins.Domain.Models;

namespace MinecraftSkins.Services.Interfaces.IRepositories
{
    public interface IExchangeRateRepository
    {
        Task AddRateAsync(ExchangeRate rate, CancellationToken cancellationToken);
        Task<ExchangeRate?> GetLastRateAsync(CancellationToken cancellationToken);
    }
}