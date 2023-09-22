using Catalog.Domain.Shared;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Catalog.Domain
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(CatalogDomainSharedModule)
		)]
	public class CatalogDomainModule : AbpModule
	{
	}
}