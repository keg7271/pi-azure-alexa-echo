#r "Newtonsoft.Json"

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static void Run(string eventHubMessage, out object factDocument, TraceWriter log)
{
    log.Verbose($"Entering ProcessIoTHub: {eventHubMessage}");

    dynamic hubMessage = JObject.Parse(eventHubMessage);

    factDocument = new
    {
        id = "MyNumber-1",
        DeviceId = hubMessage.deviceId,
        SomeValue = hubMessage.someValue,
        TimeStamp = hubMessage.timeStamp
    };

    log.Verbose($"Exiting ProcessIoTHub");
}