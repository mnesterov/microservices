using Autofac;
using Teams.Application.Commands.IdentifiedCommand.Request;
using Teams.Application.IntegrationEvents;
using Teams.Application.Mappers;
using Teams.Application.Queries;
using Teams.Infrastructure.Commands;
using Teams.Infrastructure.IntegrationEvents;
using Teams.Infrastructure.IntegrationEvents.EventLog;
using Teams.Infrastructure.IntegrationEvents.Producer;

namespace Teams.Infrastructure.Autofac
{
    public class ApplicationModule : Module
    {
        public ApplicationModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<TeamsMapper>().As<ITeamsMapper>().SingleInstance();

            builder.RegisterType<TeamsQueryService>().As<ITeamsQueryService>().InstancePerLifetimeScope();
            builder.RegisterType<CitiesQueryService>().As<ICitiesQueryService>().InstancePerLifetimeScope();

            builder.RegisterType<IdentifiedCommandRequestService>().As<IIdentifiedCommandRequestService>().InstancePerLifetimeScope();
            builder.RegisterType<IntegrationEventLogService>().As<IIntegrationEventLogService>().InstancePerLifetimeScope();
            builder.RegisterType<IntegrationEventPublishService>().As<IIntegrationEventPublishService>().InstancePerLifetimeScope();

          //  builder.RegisterType<RabbitMqIntegrationEventProducer>().As<IIntegrationEventProducer>().InstancePerLifetimeScope();
            builder.RegisterType<KafkaIntegrationEventProducer>().As<IIntegrationEventProducer>().InstancePerLifetimeScope();
        }
    }
}
