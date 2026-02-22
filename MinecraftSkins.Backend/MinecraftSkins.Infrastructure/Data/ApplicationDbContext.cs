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
                new { Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), Name = "Redstone Automaton", BasePriceUsd = 15.5m, IsAvailable = true, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
                new { Id = Guid.Parse("b2c3d4e5-f678-90ab-cdef-1234567890ab"), Name = "Diamond Sword Replica", BasePriceUsd = 25.0m, IsAvailable = true, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
                new { Id = Guid.Parse("c3d4e5f6-7890-abcd-ef12-34567890abcd"), Name = "Enderman Plush", BasePriceUsd = 12.99m, IsAvailable = true, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
                new { Id = Guid.Parse("d4e5f678-90ab-cdef-1234-567890abcdef"), Name = "Ghast Mood Lamp", BasePriceUsd = 29.5m, IsAvailable = true, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
                new { Id = Guid.Parse("e5f67890-abcd-ef12-3456-7890abcdef12"), Name = "Piggy Bank (Pig)", BasePriceUsd = 14.0m, IsAvailable = true, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
                new { Id = Guid.Parse("f67890ab-cdef-1234-5678-90abcdef1234"), Name = "Iron Golem Action Figure", BasePriceUsd = 18.75m, IsAvailable = true, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
                new { Id = Guid.Parse("7890abcd-ef12-3456-7890-abcdef123456"), Name = "Nether Portal Poster", BasePriceUsd = 8.5m, IsAvailable = true, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
                new { Id = Guid.Parse("890abcde-f123-4567-890a-bcdef1234567"), Name = "Wolf Tamed Keychain", BasePriceUsd = 5.0m, IsAvailable = true, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
                new { Id = Guid.Parse("90abcdef-1234-5678-90ab-cdef12345678"), Name = "Skeleton Archer Figurine", BasePriceUsd = 16.0m, IsAvailable = true, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow },
                new { Id = Guid.Parse("01234567-89ab-cdef-0123-456789abcdef"), Name = "Potion Bottle (Night Vision)", BasePriceUsd = 22.0m, IsAvailable = true, IsDeleted = false, CreatedAtUtc = DateTime.UtcNow });
            }

    }
}
