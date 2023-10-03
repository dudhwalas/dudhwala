using Catalog.Domain.Shared;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Catalog.Domain
{
    public class Brand : AuditedEntity<Guid>
	{
        [NotNull]
        public string? Name { get; private set; }
        [NotNull]
        public string? Image { get; private set; }
        [NotNull]
        public EnumCatalogStatus? Status { get; private set; }
        [NotNull]
        public Guid RealmId { get; private set; }

        public ICollection<Product>? Products { get; set; }

        private Brand()
		{
		}

		public Brand(Guid uid,
            [NotNull]string name,
            [NotNull] string image,
            [NotNull] EnumCatalogStatus status,
            [NotNull] Guid realmId)
		{
			Id = uid;
			SetName(name);
            SetImage(image);
            SetStatus(status);
            SetRealmId(realmId);
		}

        public Brand SetId([NotNull] Guid id)
        {
            Id = Check.NotNull(id, nameof(Id));
            if (Id == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty", nameof(Id));
            return this;
        }

        public Brand SetName([NotNull]string name)
		{
			Name = Check.NotNullOrWhiteSpace(name,nameof(Name));
			return this;
		}

        public Brand SetImage([NotNull]string image)
        {
            Image = Check.NotNullOrWhiteSpace(image, nameof(Image));
            return this;
        }

        public Brand SetStatus([NotNull]EnumCatalogStatus status)
        {
            Status = Check.NotNull(status, nameof(Status));
            return this;
        }

        public Brand SetRealmId([NotNull] Guid realmId)
        {
            RealmId = Check.NotNull(realmId, nameof(RealmId));
            if (RealmId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty", nameof(RealmId));
            return this;
        }
    }
}