using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Threading.Tasks;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace PiAzureAlexa.Skills.IoTCore
{
    public sealed class StartupTask : IBackgroundTask
    {
        static DeviceClient deviceClient;
        static string iotHubUri = "[IoT Hub Uri]";
        static string deviceKey = "[Device Key]";

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            // 
            // TODO: Insert code to perform background work
            //
            // If you start any asynchronous methods here, prevent the task
            // from closing prematurely by using BackgroundTaskDeferral as
            // described in http://aka.ms/backgroundtaskdeferral
            //
            var deferral = taskInstance.GetDeferral();

            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("RCDogzPi1", deviceKey));

            await SendDeviceToCloudMessagesAsync();

            deferral.Complete();
        }

        private static async Task SendDeviceToCloudMessagesAsync()
        {
            int someRandomValue = 7; // m/s
            Random rand = new Random();

            while (true)
            {
                someRandomValue = rand.Next(2, 12); // Generate a number or pull a value from a sensor.

                var telemetryDataPoint = new
                {
                    deviceId = "DeviceId1",         // This is unique to your collection of devices.
                    someValue = someRandomValue,    // This would be the sensor value, temp, humidity, etc.
                    dateTime = DateTime.UtcNow      // This is the moment the sensor reading was taken.
                };

                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                await deviceClient.SendEventAsync(message);

                Task.Delay(900000).Wait();  // 900000 ms = 15 min
            }
        }

    }
}
