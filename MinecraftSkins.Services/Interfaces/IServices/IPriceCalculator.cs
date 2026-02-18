namespace MinecraftSkins.Services.Interfaces.IServices
{
    public interface IPriceCalculator
    {
        decimal CalculateBtcPrice(decimal usdPrice, decimal currentRate);
    }
}