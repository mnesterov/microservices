using TeamsService.Mappers;
using TeamsService.Services;
using Infrastructure.Middlewares;
using DataAccess.EntityFramework;
using Infrastructure.Serialization.Json;
using MassTransit;
using KafkaMessageBroker;
using KafkaMessageBroker.Events;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        SetupDataAccess(builder);
        ConfigServices(builder.Services);
        SetupMassTransit(builder);

        var app = builder.Build();

        app.UseExceptionHandle();
        
        SeedData(app);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void ConfigServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ITeamsMapper, TeamsMapper>();
        serviceCollection.AddScoped<ITeamsService, TeamsService.Services.TeamsService>();

        serviceCollection.AddHttpClient<IPlayersDataClient, PlayersDataClient>();

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

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(builder.Configuration.GetConnectionString("RabbitMq"));
                cfg.ConfigureEndpoints(context);
            });

            x.AddRider(rider =>
            {
                rider.AddProducer<string, TeamRosterUpdateEvent>(KafkaTopics.TeamEventsTopic);

                rider.UsingKafka((context, cfg) =>
                {
                    cfg.Host(builder.Configuration.GetConnectionString("KafkaBroker"));
                });
            });
        });
    }

    private static void SetupDataAccess(WebApplicationBuilder builder)
    {
        var postgreSqlConnectionString = builder.Configuration.GetConnectionString("PostgreSqlDatabase");

        var currentAssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        builder.ConfigureDataAccessToPostgres(postgreSqlConnectionString, currentAssemblyName);
    }

    private static void SeedData(IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetService<AppDbContext>()!;
            dbContext.Database.Migrate();
        }
    }
}