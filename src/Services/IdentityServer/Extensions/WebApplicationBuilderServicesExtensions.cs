using IdentityServer.Serialization.Json;
using IdentityServer.Configuration;

namespace IdentityServer.Extensions
{
    public static class WebApplicationBuilderServicesExtensions
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddMvc()
                .AddJsonOptions(
                    options =>
                    {
                        options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance;
                    });


            builder.Services.AddIdentityServer(options => 
            {
                options.IssuerUri = "null";
                options.Authentication.CookieLifetime = TimeSpan.FromHours(2);
            })
                .AddInMemoryClients(Config.GetClients(builder.Configuration))
                .AddInMemoryIdentityResources(Config.GetResources())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryApiScopes(Config.GetApiScopes())
                .AddTestUsers(Config.GetTestUsers())
                .AddDeveloperSigningCredential();

            return builder;
        }
    }
}
