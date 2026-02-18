using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftSkins.Services.DTO
{
    public record SkinResponse(Guid Id, string Name, decimal FinalPriceUsd, bool IsAvailable, DateTime CreatedAtUtc, DateTime? UpdatedAtUtc);
}
