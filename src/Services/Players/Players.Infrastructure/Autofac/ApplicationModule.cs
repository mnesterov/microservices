using Autofac;
using Players.Application.Commands.IdentifiedCommand.Request;
using Players.Application.IntegrationEvents;
using Players.Application.Mappers;
using Players.Application.Queries;
using Players.Domain.Models.PlayerAggregate;
using Players.Infrastructure.Commands;
using Players.Infrastructure.IntegrationEvents;
using Players.Infrastructure.IntegrationEvents.EventLog;
using Players.Infrastructure.IntegrationEvents.EventProducer;
using Players.Infrastructure.Repositories;

namespace Players.Infrastructure.Autofac
{
    public class ApplicationModule : Module
    {
        public ApplicationModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<PlayersMapper>().As<IPlayersMapper>().SingleInstance();

            builder.RegisterType<PlayersRepository>().As<IPlayersRepository>().InstancePerLifetimeScope();

            builder.RegisterType<PlayersQueryService>().As<IPlayersQueryService>().InstancePerLifetimeScope();

            builder.RegisterType<IdentifiedCommandRequestService>().As<IIdentifiedCommandRequestService>().InstancePerLifetimeScope();
            builder.RegisterType<IntegrationEventLogService>().As<IIntegrationEventLogService>().InstancePerLifetimeScope();
            builder.RegisterType<IntegrationEventService>().As<IIntegrationEventService>().InstancePerLifetimeScope();

          //  builder.RegisterType<RabbitMqIntegrationEventProducer>().As<IIntegrationEventProducer>().InstancePerLifetimeScope();
            builder.RegisterType<KafkaIntegrationEventProducer>().As<IIntegrationEventProducer>().InstancePerLifetimeScope();
        }
    }
}
