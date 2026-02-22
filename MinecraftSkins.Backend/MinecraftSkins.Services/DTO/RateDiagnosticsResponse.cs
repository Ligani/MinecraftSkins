namespace MinecraftSkins.Services.DTO
{
    public record RateDiagnosticsResponse(decimal Rate, string Source, DateTime FetchedAtUtc);
}
