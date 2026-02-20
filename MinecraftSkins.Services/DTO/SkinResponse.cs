namespace MinecraftSkins.Services.DTO
{
    public record SkinResponse(Guid Id, string Name, decimal FinalPriceUsd, bool IsAvailable, DateTime CreatedAtUtc, DateTime? UpdatedAtUtc);
}
