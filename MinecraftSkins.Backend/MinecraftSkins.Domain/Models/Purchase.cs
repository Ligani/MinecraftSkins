using MinecraftSkins.Domain.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MinecraftSkins.Domain.Models
{
    public class Purchase
    {
        public Guid Id { get; private set; }

        public Guid SkinId { get; private set; }

        public decimal PriceUsdFinal { get; private set; }

        public decimal BtcUsdRate { get; private set; }

        public DateTime PurchasedAtUtc { get; private set; }

        public Guid? BuyerId { get; private set; }

        public bool IsDeleted { get; private set; }

        public Purchase(Guid skinId, decimal priceUsdFinal, decimal btcUsdRate, Guid? buyerId)
        {

            if (skinId == Guid.Empty)
                throw new DomainException("SkinId is required.");

            if (priceUsdFinal <= 0)
                throw new DomainException("Final price must be greater than 0.");

            if (btcUsdRate <= 0)
                throw new DomainException("Rate must be positive.");

            if (buyerId == Guid.Empty)
                throw new DomainException("BuyerId is required.");

            Id = Guid.NewGuid();
            SkinId = skinId;
            PriceUsdFinal = priceUsdFinal;
            BtcUsdRate = btcUsdRate;
            BuyerId = buyerId;
            PurchasedAtUtc = DateTime.UtcNow;
            IsDeleted = false;
        }

        private Purchase() { }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
