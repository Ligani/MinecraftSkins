namespace MinecraftSkins.Extensions
{
    public static class HttpContextExtensions
    {
        public static Guid? GetBuyerId(this HttpContext context)
        {
            if (context.Items.TryGetValue("BuyerId", out var id) && id is Guid buyerId)
            {
                return buyerId;
            }
            return null;
        }
    }
}
