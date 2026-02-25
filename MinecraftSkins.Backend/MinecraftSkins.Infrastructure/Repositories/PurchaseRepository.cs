using Microsoft.EntityFrameworkCore;
using MinecraftSkins.Domain.Models;
using MinecraftSkins.Infrastructure.Data;
using MinecraftSkins.Services.DTO;
using MinecraftSkins.Services.Interfaces.IRepositories;

namespace MinecraftSkins.Infrastructure.Repositories
{
    public class PurchaseRepository : BaseRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagedResponse<Purchase>> GetPagedAsync(Guid? buyerId,bool mineOnly,Guid? skinId,DateTime? from,DateTime? to,
            int pageNumber,int pageSize,CancellationToken cancellationToken)
        {
            var query = _dbSet.AsNoTracking();

            if (buyerId.HasValue)
                query = query.Where(x => x.BuyerId == buyerId);

            if (skinId.HasValue)
                query = query.Where(x => x.SkinId == skinId);

            if (from.HasValue)
            {
                var fromUtc = DateTime.SpecifyKind(from.Value, DateTimeKind.Utc);
                query = query.Where(x => x.PurchasedAtUtc >= fromUtc);
            }

            if (to.HasValue)
            {
                var toUtc = DateTime.SpecifyKind(to.Value, DateTimeKind.Utc);
                var endOfDay = toUtc.Date.AddDays(1).AddTicks(-1);
                query = query.Where(x => x.PurchasedAtUtc <= endOfDay);
            }

            if (mineOnly)
                query = query.Where(x => x.BuyerId == buyerId);

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(x => x.PurchasedAtUtc)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResponse<Purchase>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}