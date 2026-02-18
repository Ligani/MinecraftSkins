using Microsoft.EntityFrameworkCore;
using MinecraftSkins.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                b.Property(x => x.Name).IsRequired().HasMaxLength(200);

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

                b.Property(x => x.BuyerId).IsRequired().HasMaxLength(100);

                b.HasQueryFilter(x => !x.IsDeleted);

                b.HasOne<Skin>().WithMany().HasForeignKey(x => x.SkinId).OnDelete(DeleteBehavior.Cascade);

            });
        }
    }
}
