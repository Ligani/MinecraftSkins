using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MinecraftSkins.Domain.Models
{
    public class Skin
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal BasePriceUsd { get; private set; }
        public bool IsDeleted { get; private set; }
        public bool IsAvailable { get; private set; }
        public byte[] RowVersion { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? UpdatedAtUtc { get; private set; }
        public Skin(string name, decimal basePriceUsd)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Skin name is required.");

            if (basePriceUsd <= 0)
                throw new ArgumentException("Base price must be greater than 0.");

            Id = Guid.NewGuid();
            Name = name;
            BasePriceUsd = basePriceUsd;
            IsAvailable = true;
            CreatedAtUtc = DateTime.UtcNow;
            IsDeleted = false;

        }

        public void Delete()
        {
            if (IsDeleted)
                return;

            IsDeleted = true;
            IsAvailable = false;
        }
        public void Update(string name, decimal basePriceUsd, bool isAvailable)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Skin name cannot be empty.");

            if (basePriceUsd <= 0)
                throw new ArgumentException("Price must be greater than 0.");

            Name = name;
            BasePriceUsd = basePriceUsd;
            IsAvailable = isAvailable;
            UpdatedAtUtc = DateTime.UtcNow;
        }
        public void MarkAsSold()
        {
            if (!IsAvailable)
                throw new InvalidOperationException("Skin is already sold or unavailable.");

            IsAvailable = false;
            UpdatedAtUtc = DateTime.UtcNow;
        }
        private Skin(){}
    }
}
