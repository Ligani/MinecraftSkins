using Microsoft.EntityFrameworkCore;
using MinecraftSkins.Domain.Models;

namespace MinecraftSkins.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Skin> Skins { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ExchangeRate>(b =>
            {
                b.HasKey(x => x.Id);

                b.Property(x => x.Rate).IsRequired();

                b.Property(x => x.FetchedAtUtc).IsRequired();
            });

            builder.Entity<Skin>(b =>
            {
                b.HasKey(x => x.Id);

                b.Property(x => x.Name).IsRequired().HasMaxLength(50);

                b.Property(x => x.BasePriceUsd).HasPrecision(18, 2);

                b.Property(x => x.IsAvailable).IsRequired();

                b.Property(x => x.CreatedAtUtc).IsRequired();

                b.Property(x => x.UpdatedAtUtc);

                b.HasQueryFilter(x => !x.IsDeleted);

                b.Property(x => x.RowVersion).IsRowVersion();
            });

            builder.Entity<Purchase>(b =>
            {
                b.HasKey(x => x.Id);

                b.Property(x => x.SkinId).IsRequired();

                b.Property(x => x.PriceUsdFinal).HasPrecision(18, 2);

                b.Property(x => x.BtcUsdRate).HasPrecision(18, 8);

                b.Property(x => x.PurchasedAtUtc).IsRequired();

                b.Property(x => x.BuyerId).IsRequired();

                b.HasQueryFilter(x => !x.IsDeleted);

                b.HasOne<Skin>().WithMany().HasForeignKey(x => x.SkinId).OnDelete(DeleteBehavior.Cascade);

            });

            builder.Entity<Skin>().HasData(
                new { Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440000"), Name = "Steve Classic", BasePriceUsd = 10.0m, IsAvailable = true, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
                new { Id = Guid.Parse("67f12345-a1b2-c3d4-e5f6-7890abcdef12"), Name = "Creeper Hoodie", BasePriceUsd = 15.5m, IsAvailable = true, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
                new { Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), Name = "Redstone Automaton", BasePriceUsd = 15.5m, IsAvailable = true, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow });
            }

    }
}
