using System.ComponentModel.DataAnnotations;

namespace MinecraftSkins.Services.DTO
{
    public record SkinRequest([Required(ErrorMessage = "Skin name is required")]
    [StringLength(50, ErrorMessage = "Skin name is too long (max 50 chars)")]string Name,
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]decimal BasePriceUsd,
    [Required] bool IsAvailable);
}
    