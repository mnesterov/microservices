using TeamsService.Mappers;
using TeamsService.Services;
using Infrastructure.Middlewares;
using DataAccess.EntityFramework;
using Infrastructure.Serialization.Json;

Config();

void Config()
{
    var builder = WebApplication.CreateBuilder(args);

    SetupDataAccess(builder);
    
    // Add services to the container.
    ConfigServices(builder.Services);

    var app = builder.Build();

    app.UseExceptionHandle();

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

void ConfigServices(IServiceCollection serviceCollection) 
{
    serviceCollection.AddSingleton<ITeamsMapper, TeamsMapper>();
    serviceCollection.AddScoped<ITeamsService, TeamsService.Services.TeamsService>();

    serviceCollection.AddHttpClient<IPlayersDataClient, PlayersDataClient>();

    serviceCollection
        .AddControllers()
        .AddJsonOptions(
            options => { 
                options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance;
            });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    serviceCollection.AddEndpointsApiExplorer();
    serviceCollection.AddSwaggerGen();
}

void SetupDataAccess(WebApplicationBuilder builder)
{
    var postgreSqlConnectionString = builder.Configuration.GetConnectionString("PostgreSqlDatabase");
    builder.ConfigureDataAccessToPostgres(postgreSqlConnectionString);
}
