using System.ComponentModel.DataAnnotations;

namespace MinecraftSkins.Services.DTO
{
    public record PurchaseRequest([Required(ErrorMessage = "Skin ID is required")]Guid SkinId
);
}
