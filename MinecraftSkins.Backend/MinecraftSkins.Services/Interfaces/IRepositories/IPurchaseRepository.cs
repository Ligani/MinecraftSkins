using MinecraftSkins.Domain.Models;
using MinecraftSkins.Services.DTO;

namespace MinecraftSkins.Services.Interfaces.IRepositories
{
    public interface IPurchaseRepository : IGenericRepository<Purchase>
    {
        Task<PagedResponse<Purchase>> GetPagedAsync(Guid? buyerId, bool mineOnly, Guid? skinId, DateTime? from, DateTime? to,
            int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}