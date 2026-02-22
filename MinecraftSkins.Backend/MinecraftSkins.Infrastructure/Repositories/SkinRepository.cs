using Microsoft.EntityFrameworkCore;
using MinecraftSkins.Domain.Models;
using MinecraftSkins.Infrastructure.Data;
using MinecraftSkins.Services.DTO;
using MinecraftSkins.Services.Interfaces.IRepositories;

namespace MinecraftSkins.Infrastructure.Repositories
{
    public class SkinRepository : ISkinRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SkinRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Skin skin, CancellationToken cancellationToken)
        {
            await _dbContext.Skins.AddAsync(skin, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<PagedResponse<Skin>> GetPagedAsync(bool availableOnly, string? search, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _dbContext.Skins.Where(x => !x.IsDeleted).AsNoTracking();

            if (availableOnly)
            {
                query = query.Where(x => x.IsAvailable);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.Name.Contains(search));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query.OrderBy(x => x.CreatedAtUtc)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResponse<Skin>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<Skin?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Skins.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Skin skin, CancellationToken cancellationToken)
        {
            _dbContext.Skins.Update(skin);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> TryMarkAsSoldAsync(Guid skinId, CancellationToken ct)
        {
            var affectedRows = await _dbContext.Skins
                .Where(s => s.Id == skinId && s.IsAvailable && !s.IsDeleted)
                .ExecuteUpdateAsync(setters => setters.SetProperty(s => s.IsAvailable, false), ct);

            return affectedRows > 0;
        }
    }
}
