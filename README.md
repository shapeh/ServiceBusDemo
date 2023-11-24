# ServiceBusDemo 

What is this? 
This code demonstrates how to create an Azure Service Bus complete with a sender and a receiver.

The code combines the code from the IAmTimCorey Youtube video and the Microsoft quickstart.

Upgraded to .NET 8.0 and using the NuGet package `Azure.Messaging.ServiceBus` instead of the deprecated `Microsoft.Azure.ServiceBus` NuGet package.

**Guides**
- IAmTimCorey Youtube video: https://www.youtube.com/watch?v=v52yC9kq0Yg
- Quickstart: Send and receive messages from an Azure Service Bus queue (.NET): https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dotnet-get-started-with-queues

**Setup**
You need to provide your own `ConnectionString` to Azure Service Bus (and set up an Azure Service Bus of course). Next copy the `ConnectionString` to the `appSettings.json` file in the `SBSender`-project, as well as the `Program.cs` in the `SBReceiver`-project.