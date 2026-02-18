using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftSkins.Services.DTO
{
    public record PurchaseResponse(Guid Id, Guid SkinId, decimal FinalPrice, decimal Rate, DateTime PurchacedAt);
}
