using Catalog.Domain.Shared;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Catalog.Domain
{
    public class Product : AuditedEntity<Guid>
	{
        [NotNull]
        public string? Name { get; private set; }
        [NotNull]
        public string? Image { get; private set; }
        [NotNull]
        public EnumCatalogStatus? Status { get; private set; }
        [NotNull]
        public Guid RealmId { get; private set; }
        [NotNull]
        public Guid BrandId { get; private set; }
        public Brand? Brand { get; set; }

        private Product()
		{
		}

		public Product(Guid uid,
            [NotNull]string name,
            [NotNull] string image,
            [NotNull] EnumCatalogStatus status,
            [NotNull] Guid realmId,
            [NotNull] Guid brandId)
		{
			Id = uid;
			SetName(name);
            SetImage(image);
            SetStatus(status);
            SetRealmId(realmId);
            SetBrandId(brandId);
        }

        public Product SetId([NotNull] Guid id)
        {
            Id = Check.NotNull(id, nameof(Id));
            return this;
        }

        public Product SetName([NotNull]string name)
		{
			Name = Check.NotNullOrWhiteSpace(name,nameof(Name));
			return this;
		}

        public Product SetImage([NotNull]string image)
        {
            Image = Check.NotNullOrWhiteSpace(image, nameof(Image));
            return this;
        }

        public Product SetStatus([NotNull]EnumCatalogStatus status)
        {
            Status = Check.NotNull(status, nameof(Status));
            return this;
        }

        public Product SetRealmId([NotNull] Guid realmId)
        {
            RealmId = Check.NotNull(realmId, nameof(RealmId));
            if (RealmId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty", nameof(RealmId));
            return this;
        }

        public Product SetBrandId([NotNull] Guid brandId)
        {
            BrandId = Check.NotNull(brandId, nameof(BrandId));
            if (BrandId == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty", nameof(BrandId));
            return this;
        }
    }
}