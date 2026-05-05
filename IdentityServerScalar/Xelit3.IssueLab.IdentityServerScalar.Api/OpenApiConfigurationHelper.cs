using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;
using Scalar.AspNetCore;

namespace Xelit3.IssueLab.IdentityServerScalar.Api;

public static class OpenApiConfigurationHelper
{
    public static IServiceCollection AddCustomOpenApi(this IServiceCollection services, string authority = "https://localhost:5001")
    {
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, ct) =>
            {
                document.Components ??= new();
                document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

                // OAuth2 Authorization Code + PKCE (for Scalar login)
                document.Components.SecuritySchemes["OAuth2"] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Description = "OAuth2 Authorization Code Flow with PKCE",
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{authority}/connect/authorize"),
                            TokenUrl = new Uri($"{authority}/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "openid", "OpenID" },
                                { "profile", "Profile" },
                                { "api.read", "Weather API Read" } 
                            }
                        }
                    }
                };

                // Bearer JWT (what the API actually expects)
                document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Bearer token"
                };

                return Task.CompletedTask;
            });

            options.AddOperationTransformer((operation, context, ct) =>
            {
                var hasAllowAnonymous = context.Description.ActionDescriptor.EndpointMetadata
                    .OfType<AllowAnonymousAttribute>()
                    .Any();

                if (!hasAllowAnonymous)
                {
                    operation.Security ??= [];
                    operation.Security.Add(new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecuritySchemeReference("OAuth2", context.Document)] = ["openid", "profile", "api.read"]
                    });
                }

                return Task.CompletedTask;
            });
        });

        return services;
    }



    public static void ConfigureOpenApiWithScalarUI(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(opt =>
            {
                opt
                    .WithTitle("Weather API")
                    .WithTheme(ScalarTheme.Kepler)
                    .AddPreferredSecuritySchemes("OAuth2")
                    .AddOAuth2Flows("OAuth2", flow =>
                    {
                        flow.AuthorizationCode = new AuthorizationCodeFlow
                        {
                            ClientId = "weather.api.scalar.client",
                            Pkce = Pkce.Sha256,
                            SelectedScopes = ["openid", "profile", "api.read"],
                            RedirectUri = $"https://localhost:7125/scalar/"
                        };
                        flow.AuthorizationCode.WithCredentialsLocation(CredentialsLocation.Body);
                    });
            });
        }
    }

}
