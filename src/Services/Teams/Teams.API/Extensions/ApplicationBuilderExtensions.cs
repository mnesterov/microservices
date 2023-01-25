using Microsoft.EntityFrameworkCore;
using Npgsql;
using Polly;
using Teams.Infrastructure;

namespace Teams.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder MigrateData(this IApplicationBuilder app)
        {
            var logger = app.ApplicationServices.GetRequiredService<ILogger<AppDbContext>>();

            var retries = 10;
            var retry = Policy.Handle<NpgsqlException>()
                .WaitAndRetry(
                    retryCount: retries,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, $"--> Migration error. {retry} of {retries} attemp to connect to the database fails with: {exception.Message}");
                    });

            retry.Execute(() =>
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<AppDbContext>()!;
                    dbContext.Database.Migrate();
                }
            });

            return app;
        }
    }
}
