using DataAccess.DummyData.Repositories;
using Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.DummyData;

public static class Module
{
    public static void ConfigureDataAccessToDummyData(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITeamsRepository, DummyTeamsRepository>();
        builder.Services.AddScoped<IPlayersRepository, DummyPlayersRepository>(); 
    }
}