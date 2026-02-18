using MinecraftSkins__BTC_indexed_pricing_.DTO;

namespace MinecraftSkins.Services.Interfaces.IServices
{
    public interface IRateDiagnosticsService
    {
        Task<RateDiagnosticsResponse> GetDiagnosticsAsync(CancellationToken cancellationToken);
    }
}