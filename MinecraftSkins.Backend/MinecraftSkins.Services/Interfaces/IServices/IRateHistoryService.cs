using MinecraftSkins.Domain.Models;

namespace MinecraftSkins.Services.Interfaces.IServices
{
    public interface IRateHistoryService
    {
        Task AddRateAsync(decimal rate, CancellationToken cancellationToken);
        Task<ExchangeRate?> GetLastRateAsync(CancellationToken cancellationToken);
    }
}