using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Players.API.Filters;
using Players.API.Serialization.Json;

namespace Players.API.Extensions
{
    public static class WebApplicationBuilderServicesExtensions
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddCors(options =>
                {
                    options.AddPolicy("any", policy =>
                    {
                        policy.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });

                    var webAggregatorUrl = builder.Configuration.GetValue<string>("Urls:WebAggregator");
                    options.AddPolicy("webAggregator", policy =>
                    {
                        policy.WithOrigins(webAggregatorUrl)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
                });

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
                    options.Audience = "players";
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var identityServerExternalUrl = builder.Configuration.GetValue<string>("Urls:IdentityServerExternal");

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"{identityServerExternalUrl}/connect/authorize"),
                            TokenUrl = new Uri($"{identityServerExternalUrl}/connect/token"),

                            Scopes = new Dictionary<string, string>()
                            {
                                { "players.fullaccess", "Players Service Full Access" }
                            }
                        }
                    }
                });

                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            return builder;
        }
    }
}
