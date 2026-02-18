using Microsoft.EntityFrameworkCore;
using MinecraftSkins.Domain.Models;
using MinecraftSkins.Infrastructure.Data;
using MinecraftSkins.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftSkins.Infrastructure.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PurchaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Purchase> AddAsync(Purchase purchase, CancellationToken cancellationToken)
        {
            await _dbContext.Purchases.AddAsync(purchase, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return purchase;
        }

        public async Task<PagedResponse<Purchase>> GetPagedAsync(
            Guid? buyerId,
            bool mineOnly,
            Guid? skinId,
            DateTime? from,
            DateTime? to,
            int pageNumber,
            int pageSize, 
            CancellationToken cancellationToken)
        {
            var query = _dbContext.Purchases.Where(x => !x.IsDeleted).AsNoTracking();

            var totalCount = await query.CountAsync(cancellationToken);

            if (buyerId.HasValue)
                query = query.Where(x => x.BuyerId == buyerId);

            if (skinId.HasValue)
                query = query.Where(x => x.SkinId == skinId);

            if (from.HasValue)
                query = query.Where(x => x.PurchasedAtUtc >= from);

            if (to.HasValue)
                query = query.Where(x => x.PurchasedAtUtc <= to);

            var items = await query.OrderBy(x => x.PurchasedAtUtc)
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

        public async Task<Purchase?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Purchases.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
        public async Task UpdateAsync(Purchase purchase, CancellationToken cancellationToken)
        {
            _dbContext.Purchases.Update(purchase);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
