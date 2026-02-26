using MinecraftSkins.Domain.Models;
using MinecraftSkins.Services.DTO;

namespace MinecraftSkins.Services.Interfaces.IRepositories
{
    public interface ISkinRepository : IGenericRepository<Skin>
    {
        Task<PagedResponse<Skin>> GetPagedAsync(bool availableOnly, string? search, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<bool> TryMarkAsSoldAsync(Skin skin, CancellationToken cancellationToken);
    }
}