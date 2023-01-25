using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Players.Application.IntegrationEvents.Events;

namespace Players.Infrastructure.IntegrationEvents.EventProducer
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
                if (@event is TradePlayerIntegrationEvent)
                {
                    producer = scope.ServiceProvider.GetService<ITopicProducer<string, TradePlayerIntegrationEvent>>();
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
