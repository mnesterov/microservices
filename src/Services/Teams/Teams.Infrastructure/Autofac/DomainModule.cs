using Autofac;
using Teams.Domain.Repositories;
using Teams.Domain.Services;
using Teams.Infrastructure.Repositories;

namespace Teams.Infrastructure.Autofac
{
    public class DomainModule : Module
    {
        public DomainModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<TeamsRepository>().As<ITeamsRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CitiesRepository>().As<ICitiesRepository>().InstancePerLifetimeScope();

            builder.RegisterType<TradeService>().As<ITradeService>().InstancePerLifetimeScope();

            builder.RegisterType<TradeValidator>().As<ITradeValidator>().InstancePerLifetimeScope();
        }
    }
}
