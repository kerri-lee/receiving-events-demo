// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Core.Serialization;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.EventGrid;
using Azure.Messaging.EventGrid.SystemEvents;

namespace ReceivingEventsDemo
{
    public class ReceivingEventsDemo
    {
        public static async Task Main()
        {
            while (true)
            {
                await ReceiveEvents();
            }
        }

        public static async Task ReceiveEvents()
        {
            // Create ServiceBusClient and Receiver to receive messages
            ServiceBusClient serviceBusClient = new ServiceBusClient(Environment.GetEnvironmentVariable("EG_DEMO_CONNECTIONSTRING"));
            ServiceBusReceiver serviceBusReceiver = serviceBusClient.CreateReceiver("eg-demo");

            IReadOnlyList<ServiceBusReceivedMessage> messages = await serviceBusReceiver.ReceiveMessagesAsync(maxMessages: 40);

            // Add/update mappings for custom event payloads, specify custom ObjectSerializer
            EventGridConsumerOptions consumerOptions = new EventGridConsumerOptions();
            consumerOptions.CustomEventTypeMappings.Add("Microsoft.Demo.ClassTime", typeof(CourseEventData));
            consumerOptions.DataSerializer = new JsonObjectSerializer(
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            EventGridConsumer consumer = new EventGridConsumer(consumerOptions);
            foreach (ServiceBusReceivedMessage message in messages)
            {
                // Events are delivered as JSON
                CloudEvent[] events = consumer.DeserializeCloudEvents(message.Body.ToString());
                await serviceBusReceiver.CompleteMessageAsync(message);

                switch (events[0].Data)
                {
                    case CourseEventData scheduledCourse:
                        Console.WriteLine(scheduledCourse);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case StorageBlobCreatedEventData blobCreatedEventData:
                        Console.WriteLine("A storage blob was created: " + blobCreatedEventData.BlobType + " url: " + blobCreatedEventData.Url);
                        break;
                    case StorageBlobDeletedEventData blobDeletedEventData:
                        Console.WriteLine("A storage blob was deleted: " + blobDeletedEventData.BlobType + " url: " + blobDeletedEventData.Url);
                        break;
                }
            }
        }
    }
}
