namespace UdemyMS.Common.Utilities;
public static class Constants
{
    public struct IdentityServer
    {
        public const string ConfigName = "IdentityServer";
        public const string LogFilterName = "Duende";

        public struct Resources
        {
            public const string Catalog = "resource_catalog";
            public const string PhotoStock = "resource_photo_stock";
            public const string Basket = "resource_basket";
        }

        public struct Permissions
        {
            public const string Catalog = "catalog_fullpermission";
            public const string PhotoStock = "photo_stock_fullpermission";
            public const string Basket = "basket_fullpermission";
        }
    }

    public struct Identity
    {
        public struct Claim
        {
            public const string Sub = "sub";
        }
    }
}