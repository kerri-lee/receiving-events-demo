# Demo for Receiving Events from Event Grid .NET SDK

This is a small demo for receiving and deserializing events from Event Grid.

First, a Service Bus client and receiver for a specific Service Bus queue that events have already been routed to is created. The Event Grid consumer will parse the event envelope and attempt to deserialize event data based on existing system model types and any registered custom event types.
