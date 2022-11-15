using DataAccess.EntityFramework;
using Infrastructure.Formatters.Json;
using PlayersService.Mappers;
using PlayersService.Services;

Config();

void Config() 
{
    var builder = WebApplication.CreateBuilder(args);

    builder.ConfigureDataAccessToDummyData();
    ConfigServices(builder.Services);

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

void ConfigServices(IServiceCollection serviceCollection) 
{
    serviceCollection.AddSingleton<IPlayersMapper, PlayersMapper>();
    serviceCollection.AddScoped<IPlayersService, PlayersService.Services.PlayersService>();

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


