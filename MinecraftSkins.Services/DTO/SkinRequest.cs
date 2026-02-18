using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftSkins.Services.DTO
{
    public record SkinRequest(string Name, decimal BasePriceUsd, bool IsAvailable);
}
