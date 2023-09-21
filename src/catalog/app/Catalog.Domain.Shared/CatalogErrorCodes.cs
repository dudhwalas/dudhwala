namespace Catalog.Domain.Shared
{
    public static class CatalogErrorCodes
	{
        public const string Brand_NameAlreadyExist = "Brand:00001";
        public const string Brand_NotAvailable = "Brand:00002";
        public const string Brand_UpdateFailed = "Brand:00003";
        public const string Brand_CreateFailed = "Brand:00004";
        public const string Brand_InvalidPageTokenPageSize = "Brand:00005";
        public const string Brand_UpdateFailedMissingBrandFields = "Brand:00006";
        public const string Brand_InvalidSortFields = "Brand:00007";

        public const string File_Not_Found = "File:00001";
        public const string File_Invalid_Argument = "File:00002";
    }
}