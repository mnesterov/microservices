using Teams.API.Extensions;

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
                    options.OAuthClientId("teamsswaggerui");
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




