using Confluent.Kafka;
using MassTransit;
using Players.Application.IntegrationEvents.Consumers;
using Players.Application.IntegrationEvents.Events;

namespace Players.API.Extensions
{
    public static class WebApplicationBuilderMassTransitExtensions
    {
        public static WebApplicationBuilder ConfigureMassTransit(this WebApplicationBuilder builder)
        {
            var kafkaTopicPrefix = builder.Configuration["Kafka:TopicPrefix"];

            builder.Services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(builder.Configuration.GetConnectionString("RabbitMq"));
                    cfg.ConfigureEndpoints(context);
                });

                x.AddRider(rider =>
                {
                    rider.AddProducer<string, TradePlayerIntegrationEvent>(FormatKafkaTopicName<TradePlayerIntegrationEvent>(kafkaTopicPrefix));

                    rider.AddConsumer<CancelTradeIntegrationEventConsumer>();

                    rider.UsingKafka((context, cfg) =>
                    {
                        cfg.Host(builder.Configuration.GetConnectionString("KafkaBroker"));

                        cfg.TopicEndpoint<string, CancelTradeIntegrationEvent>(
                            FormatKafkaTopicName<CancelTradeIntegrationEvent>(kafkaTopicPrefix),
                            "players-api-group",
                            topicConfig =>
                            {
                                topicConfig.CreateIfMissing();

                                topicConfig.AutoOffsetReset = AutoOffsetReset.Earliest;

                                topicConfig.ConfigureConsumer<CancelTradeIntegrationEventConsumer>(context);
                            });
                    });
                });
            });

            return builder;
        }

        private static string FormatKafkaTopicName<TEvent>(string prefix) where TEvent : IIntegrationEvent
        {
            return $"{prefix}-{typeof(TEvent).Name}";
        }
    }

}
