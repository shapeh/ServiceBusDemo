namespace SBReceiver;

internal static class Program
{
    private const string ConnectionString = "<ConnectionStringHere>";
    private static readonly ServiceBusClientOptions ClientOptions = new() { TransportType = ServiceBusTransportType.AmqpWebSockets };
    private static readonly ServiceBusClient Client = new(ConnectionString, ClientOptions);
    private static ServiceBusProcessor _processor;

    private static async Task Main(string[] args)
    {

        _processor = Client.CreateProcessor("PersonQueue", new ServiceBusProcessorOptions()
        {
            AutoCompleteMessages = false,
            MaxConcurrentCalls = 1
        });

        try
        {
            // add handler to process messages
            _processor.ProcessMessageAsync += MessageHandler;

            // add handler to process any errors
            _processor.ProcessErrorAsync += ErrorHandler;

            // start processing 
            await _processor.StartProcessingAsync();

            Console.WriteLine("Wait for a minute and then press any key to end the processing");
            Console.ReadKey();

            // stop processing 
            Console.WriteLine("\nStopping the receiver...");
            await _processor.StopProcessingAsync();
            Console.WriteLine("Stopped receiving messages");

        }
        finally
        {
            // Calling DisposeAsync on client types is required to ensure that network
            // resources and other unmanaged objects are properly cleaned up.
            await _processor.DisposeAsync();
            await Client.DisposeAsync();
        }
    }
    static async Task MessageHandler(ProcessMessageEventArgs args)
    {
        var data = Encoding.UTF8.GetString(args.Message.Body);
        var person = JsonSerializer.Deserialize<PersonModel>(data);
        Console.WriteLine($"Person received: {person.FirstName} {person.LastName}");

        // complete the message. message is deleted from the queue. 
        await args.CompleteMessageAsync(args.Message);
    }

    // handle any errors when receiving messages
    static Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }

}