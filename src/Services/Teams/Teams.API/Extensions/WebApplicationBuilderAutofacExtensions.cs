using Autofac.Extensions.DependencyInjection;
using Autofac;
using Teams.Infrastructure.Autofac;

namespace Teams.API.Extensions
{
    public static class WebApplicationBuilderAutofacExtensions
    {
        public static WebApplicationBuilder ConfigureAutofac(this WebApplicationBuilder builder)
        {
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule(new DomainModule());
                builder.RegisterModule(new ApplicationModule());
                builder.RegisterModule(new MediatorModule());
            });

            return builder;
        }
    }
}
