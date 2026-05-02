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
            //new Client
            //{
            //    ClientId = "weather.api.scalar.client",
            //    ClientName = "Scalar API Client",

            //    AllowedGrantTypes = GrantTypes.ClientCredentials,
            //    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

            //    AllowedScopes = { "api.read" }
            //},

            new Client
            {
                ClientId = "weather.api.scalar.client",
                ClientName = "Scalar API Client",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                RedirectUris = { "https://localhost:7125/scalar/oauth/callback" },
                AllowedCorsOrigins = { "https://localhost:7125", "https://localhost:5001" },
                AllowedScopes =
                {
                    "openid", "profile", "api.read"
                },
                AllowOfflineAccess = false
            }
        ];
}
