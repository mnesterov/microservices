using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using WebHttpAggregator.Extensions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.AddOcelot("Configuration/Ocelot/", hostingContext.HostingEnvironment);
        });

        builder
            .ConfigureServices()
            .SetupConfigs();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseOcelot();

        app.Run();
    }
}
