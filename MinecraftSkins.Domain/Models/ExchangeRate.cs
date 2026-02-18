using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                throw new ArgumentException("Rate must be positive.");

            Id = Guid.NewGuid();
            Rate = rate;
            FetchedAtUtc = DateTime.UtcNow;
        }

        private ExchangeRate() { } 
    }

}
