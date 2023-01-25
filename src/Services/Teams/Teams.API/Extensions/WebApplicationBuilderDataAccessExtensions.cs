using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Teams.Infrastructure;

namespace Teams.API.Extensions
{
    public static class WebApplicationBuilderDataAccessExtensions
    {
        public static WebApplicationBuilder SetupDataAccess(this WebApplicationBuilder builder)
        {
            var postgreSqlConnectionString = builder.Configuration.GetConnectionString("PostgreSql");
            var currentAssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(postgreSqlConnectionString,
                b => ConfigurateMigrations(b, currentAssemblyName)),
                ServiceLifetime.Scoped);

            return builder;
        }

        private static NpgsqlDbContextOptionsBuilder ConfigurateMigrations(NpgsqlDbContextOptionsBuilder b, string? migrationsAssembly = null)
        {
            return migrationsAssembly != null
                ? b.MigrationsAssembly(migrationsAssembly)
                : b;
        }
    }
}
