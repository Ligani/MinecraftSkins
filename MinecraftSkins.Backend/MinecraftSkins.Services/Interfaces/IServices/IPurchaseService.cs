using MinecraftSkins.Services.DTO;

namespace MinecraftSkins.Services.Interfaces.IServices
{
    public interface IPurchaseService
    {
        Task<PurchaseResponse> CreateAsync(Guid skinId, Guid? buyerId, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<PurchaseResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<PagedResponse<PurchaseResponse>> GetPagedAsync(Guid? buyerId,bool mineOnly,Guid? skinId,DateTime? from,DateTime? to,
            int pageNumber, int pageSize,CancellationToken cancellationToken);
    }
}