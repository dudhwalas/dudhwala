namespace Catalog.Domain.Shared
{
    public static class CatalogErrorCodes
	{
        public const string BrandNameAlreadyExist = "Brand:00001";
        public const string NoBrandAvailable = "Brand:00002";
        public const string UpdateBrandFailed = "Brand:00003";
        public const string CreateBrandFailed = "Brand:00004";
        public const string InvalidPageTokenPageSize = "Brand:00005";
        public const string UpdateMissingBrandFields = "Brand:00006";
    }
}