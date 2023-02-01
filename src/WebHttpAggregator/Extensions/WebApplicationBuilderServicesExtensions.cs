using Teams.API.Services;
using WebHttpAggregator.Services.Players;
using WebHttpAggregator.Services.Teams;
using WebHttpAggregator.Services;
using WebHttpAggregator.Serialization.Json;
using Ocelot.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebHttpAggregator.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient<IHttpApiClient, HttpApiClient>();

            builder.Services.AddScoped<IPlayersService, PlayersService>();
            builder.Services.AddScoped<ITeamsService, TeamsService>();

            builder.Services
                .AddMvc()
                .AddJsonOptions(
                    options =>
                    {
                        options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance;
                    });

            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Authority = builder.Configuration.GetValue<string>("Urls:IdentityServer");
                    options.Audience = "webagg";
                });

            builder.Services.AddOcelot();
            builder.Services.AddSwaggerForOcelot(builder.Configuration);

            return builder;
        }
    }
}
