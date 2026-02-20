using MinecraftSkins.Domain.Models;
using MinecraftSkins.Services.DTO;

namespace MinecraftSkins.Services.Interfaces.IRepositories
{
    public interface IPurchaseRepository
    {
        Task<Purchase> AddAsync(Purchase purchase, CancellationToken cancellationToken);
        Task<Purchase?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<PagedResponse<Purchase>> GetPagedAsync(Guid? buyerId,bool mineOnly,Guid? skinId,DateTime? from,DateTime? to,
            int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task UpdateAsync(Purchase purchase, CancellationToken cancellationToken);
    }
}