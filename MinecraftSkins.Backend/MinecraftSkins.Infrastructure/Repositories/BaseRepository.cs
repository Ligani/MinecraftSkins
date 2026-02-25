using Microsoft.EntityFrameworkCore;
using MinecraftSkins.Domain.Common;
using MinecraftSkins.Infrastructure.Data;
using MinecraftSkins.Services.Interfaces.IRepositories;

namespace MinecraftSkins.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        protected BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity; 
        }

        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity is ISoftDeletable softDeletable)
            {
                softDeletable.Delete();
                _dbSet.Update(entity);
            }
            else
            {
                _dbSet.Remove(entity);
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
