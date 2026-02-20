using MinecraftSkins.Domain.Exceptions;

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
                throw new DomainException("Skin name is required.");

            if (basePriceUsd <= 0)
                throw new DomainException("Base price must be greater than 0.");

            if (name.Length > 50)
                throw new DomainException("The name length cannot exceed 50 characters.");

            Id = Guid.NewGuid();
            Name = name;
            BasePriceUsd = basePriceUsd;
            IsAvailable = true;
            CreatedAtUtc = DateTime.UtcNow;
            IsDeleted = false;

        }

        private Skin() { }

        public void Delete()
        {
            IsDeleted = true;
            IsAvailable = false;
        }

        public void Update(string name, decimal basePriceUsd, bool isAvailable)
        {
            if (name.Length > 50)
                throw new DomainException("The name length cannot exceed 50 characters.");

            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Skin name is required.");

            if (basePriceUsd <= 0)
                throw new DomainException("Base price must be greater than 0.");

            Name = name;
            BasePriceUsd = basePriceUsd;
            IsAvailable = isAvailable;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        public void MarkAsSold()
        {
            if (!IsAvailable)
                throw new DomainException("Skin is already sold or unavailable.");

            IsAvailable = false;
            UpdatedAtUtc = DateTime.UtcNow;
        }
    }
}
