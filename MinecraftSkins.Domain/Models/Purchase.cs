using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftSkins.Domain.Models
{
    public class Purchase
    {
        public Guid Id { get; private set; }

        public Guid SkinId { get; private set; }

        public decimal PriceUsdFinal { get; private set; }

        public decimal BtcUsdRate { get; private set; }

        public DateTime PurchasedAtUtc { get; private set; }

        public Guid BuyerId { get; private set; }
        public bool IsDeleted { get; private set; }

        public Purchase(Guid skinId,decimal priceUsdFinal,decimal btcUsdRate,Guid buyerId)
        {
            if (skinId == Guid.Empty)
                throw new ArgumentException("SkinId is required.");

            if (priceUsdFinal <= 0)
                throw new ArgumentException("Final price must be greater than 0.");

            if (btcUsdRate <= 0)
                throw new ArgumentException("BTC/USD rate must be greater than 0.");

            if (buyerId == Guid.Empty)
                throw new ArgumentException("BuyerId is required.");

            Id = Guid.NewGuid();
            SkinId = skinId;
            PriceUsdFinal = priceUsdFinal;
            BtcUsdRate = btcUsdRate;
            BuyerId = buyerId;
            PurchasedAtUtc = DateTime.UtcNow;
            IsDeleted = false;
        }
        public void Delete()
        {
            if (IsDeleted)
                return;

            IsDeleted = true;
        }
        private Purchase() { }
    }
}
