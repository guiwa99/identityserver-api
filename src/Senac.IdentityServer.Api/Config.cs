using Duende.IdentityServer.Models;

namespace Senac.IdentityServer.Api
{
  public static class Config
  {
    public static IEnumerable<ApiScope> ApiScopes =>
        [
            new ApiScope(name: "loca-games-api", displayName: "Loca Games api")
        ];

    public static IEnumerable<Client> Clients =>
        [
            new Client
            {
                ClientId = "client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "loca-games-api" }
            }
        ];
  }
}