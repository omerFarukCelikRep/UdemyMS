using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace UdemyMS.ExternalServices.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources => Array.Empty<IdentityResource>();

    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        new ApiResource("resource_catalog"){Scopes = { "catalog_fullpermission" } },
        new ApiResource("resource_photo_stock"){Scopes = { "photo_stock_fullpermission" } },
        new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
    };

    public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
    {
        new ApiScope("catalog_fullpermission","Catalog Api Tam Erişim"),
        new ApiScope("photo_stock_fullpermission","Photo Stock Api Tam Erişim"),
        new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
    };

    public static IEnumerable<Client> Clients => new Client[]
    {
        new Client
        {
            ClientId = "WebMvcClient",
            ClientName = "Asp.Net Core MVC",
            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes = { "catalog_fullpermission", "photo_stock_fullpermission",IdentityServerConstants.LocalApi.ScopeName }
        }
    };
}
