using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Teams.Application.IntegrationEvents.Events;

namespace Teams.Infrastructure.IntegrationEvents.Producer
{
    public class KafkaIntegrationEventProducer : IIntegrationEventProducer
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public KafkaIntegrationEventProducer(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Produce<T>(T @event) where T : IntegrationEvent
        {
            dynamic producer;

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                if (@event is CancelTradeIntegrationEvent)
                {
                    producer = scope.ServiceProvider.GetService<ITopicProducer<string, CancelTradeIntegrationEvent>>();
                }
                else
                {
                    throw new NotSupportedException();
                }
            }

            var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            await producer.Produce(@event.EventId.ToString(), @event, cancellationTokenSource.Token);
        }
    }
}
