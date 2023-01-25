using Autofac;
using Players.Domain.Models.PlayerAggregate;
using Players.Infrastructure.Repositories;

namespace Players.Infrastructure.Autofac
{
    public class DomainModule : Module
    {
        public DomainModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<PlayersRepository>().As<IPlayersRepository>().InstancePerLifetimeScope();
        }
    }
}
