using System;
using Catalog.Application.Contract;
using Catalog.Domain;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Catalog.Application
{
	[DependsOn(
		typeof(AbpDddApplicationModule),
		typeof(CatalogDomainModule),
		typeof(CatalogApplicationContractModule)
		)]
	public class CatalogApplicationModule : AbpModule
	{
		
	}
}