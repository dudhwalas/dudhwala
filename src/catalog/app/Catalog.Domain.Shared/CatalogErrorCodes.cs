namespace Catalog.Domain.Shared
{
    public static class CatalogErrorCodes
	{
        public const string Brand_NameAlreadyExist = "Brand:00001";
        public const string Brand_NotAvailable = "Brand:00002";
        public const string Brand_UpdateFailed = "Brand:00003";
        public const string Brand_CreateFailed = "Brand:00004";
        public const string Brand_InvalidPageTokenPageSize = "Brand:00005";
        public const string Brand_UpdateFailed_MissingBrandFields = "Brand:00006";
        public const string Brand_InvalidSortFields = "Brand:00007";

        public const string Product_NameAlreadyExist = "Product:00001";
        public const string Product_NotAvailable = "Product:00002";
        public const string Product_UpdateFailed = "Product:00003";
        public const string Product_CreateFailed = "Product:00004";
        public const string Product_InvalidPageTokenPageSize = "Product:00005";
        public const string Product_UpdateFailed_MissingProductFields = "Product:00006";
        public const string Product_InvalidSortFields = "Product:00007";
        public const string Product_BrandNotAvailable = "Product:00008";

    }
}