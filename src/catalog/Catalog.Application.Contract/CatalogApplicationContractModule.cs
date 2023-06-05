using Catalog.Domain.Shared;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Catalog.Application.Contract;

[DependsOn(
        typeof(AbpDddApplicationContractsModule),
        typeof(CatalogDomainSharedModule)
    )]
public class CatalogApplicationContractModule : AbpModule
{

}