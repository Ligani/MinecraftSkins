using MinecraftSkins.Services.DTO;

namespace MinecraftSkins.Services.Interfaces.IServices
{
    public interface IRateDiagnosticsService
    {
        Task<RateDiagnosticsResponse> GetDiagnosticsAsync(CancellationToken cancellationToken);
    }
}