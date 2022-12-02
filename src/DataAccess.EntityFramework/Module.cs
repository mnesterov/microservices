using DataAccess.EntityFramework.Repositories;
using Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace DataAccess.EntityFramework;

public static class Module
{
    public static void ConfigureDataAccessToPostgres(this WebApplicationBuilder builder, string connectionString, string? migrationsAssembly = null)
    {
        builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString, b => ConfigurateMigrations(b, migrationsAssembly)), ServiceLifetime.Scoped);

        builder.Services.AddScoped<ITeamsRepository, PostgreTeamsRepository>();    
        builder.Services.AddScoped<IPlayersRepository, PostgrePlayersRepository>();       
    }

    private static NpgsqlDbContextOptionsBuilder ConfigurateMigrations(NpgsqlDbContextOptionsBuilder b, string? migrationsAssembly = null)
    {
        return migrationsAssembly != null 
            ? b.MigrationsAssembly(migrationsAssembly)
            : b;
    }
}