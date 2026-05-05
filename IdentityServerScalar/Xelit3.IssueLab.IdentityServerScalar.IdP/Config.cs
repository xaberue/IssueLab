using Duende.IdentityServer.Models;

namespace Xelit3.IssueLab.IdentityServerScalar;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        ];

    public static IEnumerable<ApiScope> ApiScopes =>
        [
            new ApiScope("api.read")
        ];

    public static IEnumerable<ApiResource> ApiResources =>
       new[]
       {
            new ApiResource("weather.api", "Weather API")
            {
                Scopes = { "api.read" },
            }
       };

    public static IEnumerable<Client> Clients =>
        [
            new Client
            {
                ClientId = "weather.api.scalar.client",
                ClientName = "Scalar API Client",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                RedirectUris = { "https://localhost:7125/scalar/" },
                AllowedCorsOrigins = { "https://localhost:7125", "https://localhost:5001" },
                AllowedScopes =
                {
                    "openid", "profile", "api.read"
                },
                AllowOfflineAccess = false
            }
        ];
}
