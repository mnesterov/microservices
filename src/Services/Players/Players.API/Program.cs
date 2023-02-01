using Microsoft.Extensions.Options;
using Players.API.Extensions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder
            .SetupDataAccess()
            .ConfigureServices()
            .ConfigureAutofac()
            .ConfigureMassTransit();

        var app = builder.Build();

        app.MigrateData();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app
                .UseDeveloperExceptionPage()
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint($"/swagger/v1/swagger.json", "Players API v1");
                    options.OAuthClientId("playersswaggerui");
                });
        }

        app.UseCors("any");

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapControllers();
        });

        app.Run();
    }
}




