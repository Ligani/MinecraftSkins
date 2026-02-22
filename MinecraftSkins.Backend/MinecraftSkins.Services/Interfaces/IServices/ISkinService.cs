using MinecraftSkins.Services.DTO;

namespace MinecraftSkins.Services.Interfaces.IServices
{
    public interface ISkinService
    {
        Task<Guid> CreateAsync(string name, decimal price, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<SkinResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<PagedResponse<SkinResponse>> GetPagedAsync(bool availableOnly, string? search, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task UpdateAsync(Guid id, string name, decimal basePriceUsd, bool isAvailable, CancellationToken cancellationToken);
    }
}