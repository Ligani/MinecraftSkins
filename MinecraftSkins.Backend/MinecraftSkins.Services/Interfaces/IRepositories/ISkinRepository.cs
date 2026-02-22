using MinecraftSkins.Domain.Models;
using MinecraftSkins.Services.DTO;

namespace MinecraftSkins.Services.Interfaces.IRepositories
{
    public interface ISkinRepository
    {
        Task AddAsync(Skin skin, CancellationToken cancellationToken);
        Task<PagedResponse<Skin>> GetPagedAsync(bool availableOnly, string? search, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<Skin?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAsync(Skin skin, CancellationToken cancellationToken);
        Task<bool> TryMarkAsSoldAsync(Guid skinId, CancellationToken cancellationToken);
    }
}