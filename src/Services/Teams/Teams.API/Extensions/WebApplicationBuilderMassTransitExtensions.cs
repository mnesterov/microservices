using Confluent.Kafka;
using MassTransit;
using Teams.Application.IntegrationEvents.Consumers;
using Teams.Application.IntegrationEvents.Events;

namespace Teams.API.Extensions
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
                    rider.AddProducer<string, CancelTradeIntegrationEvent>(FormatKafkaTopicName<CancelTradeIntegrationEvent>(kafkaTopicPrefix));

                    rider.AddConsumer<TradePlayerIntegrationEventConsumer>();

                    rider.UsingKafka((context, cfg) =>
                    {
                        cfg.Host(builder.Configuration.GetConnectionString("KafkaBroker"));

                        cfg.TopicEndpoint<string, TradePlayerIntegrationEvent>(
                            FormatKafkaTopicName<TradePlayerIntegrationEvent>(kafkaTopicPrefix),
                            "teams-api-group",
                            topicConfig =>
                            {
                                topicConfig.CreateIfMissing();

                                topicConfig.AutoOffsetReset = AutoOffsetReset.Earliest;

                                topicConfig.ConfigureConsumer<TradePlayerIntegrationEventConsumer>(context);
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
