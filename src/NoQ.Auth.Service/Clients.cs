using System.Collections.Generic;
using IdentityServer4.Models;

namespace NoQ.Auth.Service
{
    internal class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client> {
            new Client {
                ClientId = "oauthClient",
                AccessTokenType = AccessTokenType.Reference,
                ClientName = "Example Client Credentials Client Application",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = new List<Secret> {
                    new Secret("superSecretPassword".Sha256())},
                AllowedScopes = new List<string> {"customAPI.read"}
            }
        };
        }
    }
}
