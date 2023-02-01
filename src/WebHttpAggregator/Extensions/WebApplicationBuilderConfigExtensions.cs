using WebHttpAggregator.Configuration;

namespace WebHttpAggregator.Extensions
{
    public static class WebApplicationBuilderConfigExtensions
    {
        public static WebApplicationBuilder SetupConfigs(this WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;

            builder.Services.Configure<ApiEndpointsConfig>(configuration.GetSection("ApiEndpoints"));

            return builder;
        }
    }
}
