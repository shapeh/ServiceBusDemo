namespace SBSender.Services;

public class QueueService : IQueueService
{
    private readonly IConfiguration _config;
    private ServiceBusClient _client;
    private ServiceBusSender _sender;
    private readonly ServiceBusClientOptions _clientOptions = new() { TransportType = ServiceBusTransportType.AmqpWebSockets };


    public QueueService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendMessageAsync<T>(T serviceBusMessage, string queueName, CancellationToken cancellationToken)
    {
        _client = new ServiceBusClient(_config.GetConnectionString("AzureServiceBus"), _clientOptions);
        _sender = _client.CreateSender(queueName);
        var messageBody = JsonSerializer.Serialize(serviceBusMessage);
        var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody));

        await _sender.SendMessageAsync(message, cancellationToken);
    }
}
