using System;
using System.IO;
using Newtonsoft.Json;
using raspberry_mqqt_motion_alarm.Models;

namespace raspberry_mqqt_motion_alarm.Install
{
    public static class Installation
    {
        const string DefaultInstallDir = "/usr/local/bin/rmma";

        public static void Start()
        {
            Console.WriteLine("Welcome to the raspberry mqqt motion alarm installer");
            Console.WriteLine("");

            Console.WriteLine($"The software will be installed in {DefaultInstallDir} You can edit the config .json to your specific needs");



            Console.ReadLine();

            Install();
        }

        private static void Install()
        {
            File.Copy(AppDomain.CurrentDomain.FriendlyName, Path.Combine(DefaultInstallDir, AppDomain.CurrentDomain.FriendlyName));
            string configLocation = Path.Combine(DefaultInstallDir, "config.json");            

            File.WriteAllText(configLocation, JsonConvert.SerializeObject(new Settings(
                Guid.NewGuid().ToString(),//Nog te protectten met data protection key
                "example.mqtt.com",
                "mqttuser",
                "mqttpass"), Formatting.Indented));

            Console.ReadLine();

        }

        
    }
}
