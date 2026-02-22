using MinecraftSkins.Domain.Exceptions;

namespace MinecraftSkins.Domain.Models
{
    public class ExchangeRate
    {
        public Guid Id { get; private set; }      

        public decimal Rate { get; private set; }    

        public DateTime FetchedAtUtc { get; private set; } 

        public ExchangeRate(decimal rate)
        {
            if (rate <= 0)
                throw new DomainException("Rate must be positive.");

            Id = Guid.NewGuid();
            Rate = rate;
            FetchedAtUtc = DateTime.UtcNow;
        }

        private ExchangeRate() { } 
    }

}
