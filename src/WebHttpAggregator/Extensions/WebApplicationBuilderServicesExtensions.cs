using Teams.API.Services;
using WebHttpAggregator.Services.Players;
using WebHttpAggregator.Services.Teams;
using WebHttpAggregator.Services;
using WebHttpAggregator.Serialization.Json;

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

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            return builder;
        }
    }
}
