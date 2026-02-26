using Microsoft.EntityFrameworkCore;
using MinecraftSkins.Domain.Models;
using MinecraftSkins.Infrastructure.Data;
using MinecraftSkins.Services.DTO;
using MinecraftSkins.Services.Interfaces.IRepositories;

namespace MinecraftSkins.Infrastructure.Repositories
{
    public class SkinRepository : BaseRepository<Skin>, ISkinRepository
    {
        public SkinRepository(ApplicationDbContext dbContext) : base(dbContext){}
        public async Task<PagedResponse<Skin>> GetPagedAsync(bool availableOnly, string? search, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _dbSet.Where(x => !x.IsDeleted).AsNoTracking();

            if (availableOnly)
            {
                query = query.Where(x => x.IsAvailable);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.Name.Contains(search));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query.OrderBy(x => x.CreatedAtUtc).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return new PagedResponse<Skin>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<bool> TryMarkAsSoldAsync(Skin skin, CancellationToken cancellationToken)
        {
            try
            {
                skin.MarkAsSold();
                _dbSet.Update(skin);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }
    }
}
