using DataAccess.EntityFramework.Repositories;
using Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.EntityFramework;

public static class Module
{
    public static void ConfigureDataAccessToDummyData(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITeamsRepository, DummyTeamsRepository>();
        builder.Services.AddScoped<IPlayersRepository, DummyPlayersRepository>(); 
    }

    public static void ConfigureDataAccessToPostgres(this WebApplicationBuilder builder, string connectionString)
    {
        builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Scoped);

        builder.Services.AddScoped<ITeamsRepository, PostgreTeamsRepository>();    
        builder.Services.AddScoped<IPlayersRepository, PostgrePlayersRepository>();       
    }
}