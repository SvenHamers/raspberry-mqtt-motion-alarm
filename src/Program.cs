using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using raspberry_mqqt_motion_alarm.Install;
using raspberry_mqqt_motion_alarm.Models;
using raspberry_mqqt_motion_alarm.Services;
using raspberry_mqqt_motion_alarm.Services.Implementations;
using raspberry_mqqt_motion_alarm.Services.Interfaces;
using raspberry_mqqt_motion_alarm.Worker;

namespace raspberry_mqqt_motion_alarm
{
    public class Program
    {
        public static List<MotionDetector> detectionWorkers = new();
        public static Settings appSettings = new();

        public static void Main(string[] args)
        {

           //Installation.Start();

            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .AddJsonFile("config.json")
                .Build();

            appSettings = configuration.Get<Settings>();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls(Program.appSettings.Api_Url);
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices(services =>
               {
       
                   services.AddSingleton<IMqqtAlarmService>(x => new MqqtAlarmService(
                       Program.appSettings.Mqtt_Host,
                       Program.appSettings.Mqtt_Port,
                       Program.appSettings.Mqtt_User,
                       Program.appSettings.Mqtt_Password,
                       Program.appSettings.Mqtt_Alarm_Message_Active,
                       Program.appSettings.Mqtt_Alarm_Message_InActive));
                   services.AddHostedService<DetectionWorker>();

                });
    }
}
