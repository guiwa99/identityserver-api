using System.Security.Cryptography;
using Duende.IdentityServer.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

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

    public static SigningCredentials GetPersistedKey()
    {
      var keyFile = "signingkey.json";

      if (File.Exists(keyFile))
      {
        var parameters = JsonConvert.DeserializeObject<RSAParameters>(File.ReadAllText(keyFile));
        var rsa = RSA.Create();
        rsa.ImportParameters(parameters);
        return new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);
      }
      else
      {
        var rsa = RSA.Create(2048);
        var parameters = rsa.ExportParameters(true);
        File.WriteAllText(keyFile, JsonConvert.SerializeObject(parameters));
        return new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);
      }
    }
  }

  
  }