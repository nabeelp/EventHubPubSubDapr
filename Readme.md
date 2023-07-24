# EventHubPubSubDapr

This is a sample project to demonstrate how to use Dapr to subscribe to Azure Event Hub messages, where the messages are *not* conformant to the CloudEvents schema.

## Steps

1. In your Azure portal, create an Azure Event Hub namespace and an Event Hub within it.
1. From the directory where this readme file is located, create a sub directory named `components` and create a yaml file wihtin this sub directory to contain the Dapr component for the Event Hub. 
1. Populate the yaml file with the applicable content, as per https://docs.dapr.io/reference/components-reference/supported-pubsub/setup-azure-eventhubs/
1. By default, Dapr will use the app-id of this consumer app as the name of the Event Hub consumer group when subscribing to Event Hub. If you want to use a different consumer group, or if you want to use the default consumer group, add an additional property `consumerID` in the yaml file and set this to the name of your consumer group. This value can be set to `$Default` if you are using the default consumer group.
1. Modify line 19 of `EventConsumerDapr\Controllers\ConsumerController.cs`, replacing `source-hub-component` with the name of the component created above, and replacing `source-hub` with the name of the Event Hub created.
1. To start the consumer, navigate to the `EventConsumerDapr` directory and run `dapr run --app-id 'consumer-app' --components-path ../components/ --app-port 5289 --log-level debug -- dotnet run --project .`
1. Once started navigate to the `EventPublisherConsole` directory in a seperate console window and run `.\bin\Debug\net6.0\EventPublisherConsole.exe`
