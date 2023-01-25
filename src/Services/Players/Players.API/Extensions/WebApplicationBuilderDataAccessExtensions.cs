using Microsoft.EntityFrameworkCore;
using Players.Infrastructure;

namespace Players.API.Extensions
{
    public static class WebApplicationBuilderDataAccessExtensions
    {
        public static WebApplicationBuilder SetupDataAccess(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("SqlServer");
            var currentAssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(
                        connectionString,
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(currentAssemblyName);
                            //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                        });
                });

            return builder;
        }
    }
}
