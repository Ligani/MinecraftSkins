using Microsoft.EntityFrameworkCore;
using MinecraftSkins.Domain.Models;
using MinecraftSkins.Infrastructure.Data;
using MinecraftSkins.Services.Interfaces.IRepositories;

namespace MinecraftSkins.Infrastructure.Repositories
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ExchangeRateRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ExchangeRate?> GetLastRateAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.ExchangeRates.OrderByDescending(r => r.FetchedAtUtc)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task AddRateAsync(ExchangeRate rate, CancellationToken cancellationToken)
        {
            _dbContext.ExchangeRates.Add(rate);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
