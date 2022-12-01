using Confluent.Kafka;
using DataAccess.EntityFramework;
using Infrastructure.Serialization.Json;
using KafkaMessageBroker;
using KafkaMessageBroker.Events;
using MassTransit;
using PlayersService.Consumers;
using PlayersService.Mappers;
using PlayersService.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        SetupDataAccess(builder);
        ConfigServices(builder.Services);
        SetupMassTransit(builder);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void ConfigServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IPlayersMapper, PlayersMapper>();
        serviceCollection.AddScoped<IPlayersService, PlayersService.Services.PlayersService>();

        serviceCollection
            .AddControllers()
            .AddJsonOptions(
                options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance;
                });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();
    }

    private static void SetupMassTransit(WebApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(x =>
        {
            x.SetSnakeCaseEndpointNameFormatter();

            x.AddConsumer<CreatePlayerEventConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(builder.Configuration.GetConnectionString("RabbitMq"));
                cfg.ConfigureEndpoints(context);
            });

            x.AddRider(rider =>
            {
                rider.AddConsumer<TeamRosterUpdateEventConsumer>();

                rider.UsingKafka((context, cfg) =>
                {
                    cfg.Host(builder.Configuration.GetConnectionString("KafkaBroker"));

                    var groupId = Guid.NewGuid().ToString(); // always start from beginning
                    cfg.TopicEndpoint<string, TeamRosterUpdateEvent>(KafkaTopics.TeamEventsTopic, groupId, topicConfig =>
                    {
                        topicConfig.CreateIfMissing();

                        topicConfig.AutoOffsetReset = AutoOffsetReset.Earliest;

                        topicConfig.ConfigureConsumer<TeamRosterUpdateEventConsumer>(context);
                    });
                });
            });
        });
    }

    private static void SetupDataAccess(WebApplicationBuilder builder)
    {
        var postgreSqlConnectionString = builder.Configuration.GetConnectionString("PostgreSqlDatabase");
        builder.ConfigureDataAccessToPostgres(postgreSqlConnectionString);
    }
}