using MinecraftSkins.Domain.Exceptions;
using MinecraftSkins.Services.Interfaces.IServices;

namespace MinecraftSkins.Services.Logics
{
    public class PriceCalculator : IPriceCalculator
    {
        public decimal CalculateBtcPrice(decimal usdPrice, decimal currentRate)
        {
            if (usdPrice < 0)
                throw new BusinessException("Price cannot be negative");

            const decimal BaseMargin = 0.05m;    
            const decimal RiskFactor = 0.000001m;
            const decimal MaxMargin = 0.30m;

            decimal dynamicMargin = BaseMargin + (currentRate * RiskFactor);

            decimal finalMargin = Math.Min(dynamicMargin, MaxMargin);

            decimal finalPriceUsd = usdPrice * (1 + finalMargin);

            return Math.Round(finalPriceUsd, 2, MidpointRounding.AwayFromZero);
        }
    }
}
