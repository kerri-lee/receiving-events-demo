# Demo for Receiving Events from Event Grid .NET SDK

This is a small demo for receiving and deserializing events from Event Grid.

First, a Service Bus client and receiver for a specific Service Bus queue that events have already been routed to is created. The Event Grid consumer will parse the event envelope and attempt to deserialize event data based on existing system model types and any registered custom event types.

In order to run the demo, you must first specify your own Service Bus connection string when creating the client in `ReceivingEventsDemo.cs`. Also, this demo doesn't publish any events to the Event Grid topic your Service Bus queue is subscribed to; `ReceivingEventsDemo.cs` simply receives and deserializes events. `ReceivingEventsDemo.cs` references existing projects (Azure.Core, Azure.Messaging.ServiceBus, Azure.Messaging.EventGrid).

Note: the Event Grid topic subscribed to by the SB queue in this demo accepts events of the CloudEvent v1.0 schema!
