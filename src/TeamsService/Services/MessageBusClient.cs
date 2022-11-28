using System.Text;
using System.Text.Json;
using Dtos;
using RabbitMQ.Client;

namespace TeamsService.Services;

public class MessageBusClient : IMessageBusClient
{
    private readonly IConfiguration _configuration;
    private readonly IConnectionFactory _connectionFactory;

    public MessageBusClient(IConfiguration configuration)
    {
        _configuration = configuration;

        _connectionFactory = new ConnectionFactory() 
        { 
            HostName = _configuration["RabbitMqHost"], 
            Port = int.Parse(_configuration["RabbitMqPort"]) 
        };
    }

    public void PublishCreateNewPlayer(PlayerDto.CreateData data)
    {
        var json = JsonSerializer.Serialize(data);
        var message = Encoding.UTF8.GetBytes(json);

        using (var conn = _connectionFactory.CreateConnection())
        {
            using (var channel = conn.CreateModel())
            {
                const string exchange = "broadcast";
                channel.ExchangeDeclare(exchange, ExchangeType.Fanout);
                channel.BasicPublish(exchange, routingKey: string.Empty, basicProperties: null, body: message);
            }
        }
    }
}