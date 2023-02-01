using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;
using Secret = IdentityServer4.Models.Secret;

namespace IdentityServer.Configuration
{
    public static class Config
    {
        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            var webAggregatorUrl = configuration.GetValue<string>("ClientUrls:WebHttpAggregator");
            var teamsApiUrl = configuration.GetValue<string>("ClientUrls:TeamsAPI");
            var playersApiUrl = configuration.GetValue<string>("ClientUrls:PlayersAPI");

            return new List<Client>
            {
                new Client
                {
                    ClientId = "teamsswaggerui",
                    ClientName = "Teams Swagger UI",
                    AllowedGrantTypes = { GrantType.Implicit },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{teamsApiUrl}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{teamsApiUrl}/swagger/" },

                    AllowedScopes =
                    {
                        "teams.fullaccess"
                    }
                },
                new Client
                {
                    ClientId = "playersswaggerui",
                    ClientName = "Players Swagger UI",
                    AllowedGrantTypes = { GrantType.Implicit },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{playersApiUrl}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{playersApiUrl}/swagger/" },

                    AllowedScopes =
                    {
                        "players.fullaccess"
                    }
                },
                new Client
                {
                    ClientId = "webaggswaggerui",
                    ClientName = "Web Aggregattor Swagger UI",
                    AllowedGrantTypes = { GrantType.Implicit, GrantType.ClientCredentials },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{webAggregatorUrl}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{webAggregatorUrl}/swagger/" },

                    AllowedScopes =
                    {
                        "webagg.fullaccess",
                        "players.fullaccess",
                        "teams.fullaccess"
                    }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
               new ApiScope("teams.fullaccess", "Teams Service"),
               new ApiScope("players.fullaccess", "Players Service"),
               new ApiScope("webagg.fullaccess", "Web Http Aggregator"),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "teams",
                    DisplayName = "Teams Service",
                    Scopes = { "teams.fullaccess" },
                },
                new ApiResource
                {
                    Name = "players", 
                    DisplayName = "Players Service", 
                    Scopes = { "players.fullaccess" },
                },
                new ApiResource
                { 
                    Name = "webagg", 
                    DisplayName = "Web Http Aggregator", 
                    Scopes = { "webagg.fullaccess", "players.fullaccess", "teams.fullaccess" },
                }
            };
        }

        public static IEnumerable<IdentityResource> GetResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "c70fd6b2-787e-48ef-9c5c-e1cf7022f170",
                    Username = "admin",
                    Password = "admin",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "Adam"),
                        new Claim(JwtClaimTypes.FamilyName, "Silver")
                    }
                }
            };
        }
    }
}
