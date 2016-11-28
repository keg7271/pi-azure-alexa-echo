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
        id = "Fact-1",
        DeviceId = hubMessage.deviceId,
        DiceRole = hubMessage.diceRole,
        DateTime = hubMessage.dateTime
    };

    log.Verbose($"Exiting ProcessIoTHub");
}