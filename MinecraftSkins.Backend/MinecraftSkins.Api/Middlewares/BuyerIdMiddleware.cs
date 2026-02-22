namespace MinecraftSkins.Middlewares
{
    public class BuyerIdMiddleware
    {
        private readonly RequestDelegate _next;
        private const string HeaderName = "X-User-Id";

        public BuyerIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(HeaderName, out var headerValue))
            {
                if (Guid.TryParse(headerValue, out var buyerId))
                {
                    context.Items["BuyerId"] = buyerId;
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync($"Invalid format for {HeaderName}. Expected GUID.");
                    return;
                }
            }

            await _next(context);
        }
    }
}
