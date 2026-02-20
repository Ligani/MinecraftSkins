namespace MinecraftSkins.Services.DTO
{
    public record PurchaseResponse(Guid Id, Guid SkinId, decimal FinalPrice, decimal Rate, DateTime PurchacedAt);
}
