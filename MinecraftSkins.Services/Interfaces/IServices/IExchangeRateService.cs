namespace MinecraftSkins.Services.Interfaces.IServices
{
    public interface IExchangeRateService
    {
        Task<decimal> GetRateAsync(CancellationToken cancellationToken);
    }
}