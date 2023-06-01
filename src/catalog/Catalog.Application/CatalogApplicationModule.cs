using System;
using Catalog.Domain;
using Volo.Abp.Modularity;

namespace Catalog.Application
{
	[DependsOn(typeof(CatalogDomainModule))]
	public class CatalogApplicationModule : AbpModule
	{
		
	}
}

