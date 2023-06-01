using System;
using Catalog.Domain.Shared;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Catalog.Domain
{
	public class Brand : Entity<Guid>
	{
		public string? Name { get; private set; }

		private Brand()
		{
		}

		public Brand(Guid _uid, [NotNull]string _name)
		{
			Id = _uid;
			SetName(_name);
			
		}

		public Brand SetName(string _name)
		{
			if(string.IsNullOrEmpty(_name))
				throw new BusinessException(CatalogErrorCodes.BrandNameMissing);
			return this;
		}
	}
}

