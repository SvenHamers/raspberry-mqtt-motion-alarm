using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Device.Gpio;
using raspberry_mqqt_motion_alarm.Services.Interfaces;

namespace raspberry_mqqt_motion_alarm.Worker
{


public class DetectionWorker : IHostedService, IDisposable {

        readonly ILogger<DetectionWorker> logger;
        readonly IMqqtAlarmService mqqtService;
        readonly GpioController controller = new();

        private Timer timer;

        public DetectionWorker(
            ILogger<DetectionWorker> logger,
            IMqqtAlarmService mqqtService
            )
        {
            this.mqqtService = mqqtService;
            this.logger = logger;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            this.logger.LogInformation("DetectionWorker Service running.");

            this.timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }


        private void OnSignalPinRiseEvent(object sender, PinValueChangedEventArgs args)
        {

            this.logger.LogInformation($"Moition detected on pin {args.PinNumber} at {DateTime.Now}");

            Program.detectionWorkers.ForEach(x =>
            {
                if (x.RaspBerryPin != args.PinNumber)
                    return;

                x.AlarmActive = true;
                x.LastTriggered = DateTime.Now;
                mqqtService.Alarm(x.Name);

            });
        }

        private void StartMonitoring(int pin)
        {

            if (this.controller.IsPinOpen(pin) == true)
                return;

            this.controller.OpenPin(pin, PinMode.Input);
            this.controller.RegisterCallbackForPinValueChangedEvent(pin, PinEventTypes.Rising, OnSignalPinRiseEvent);

        }


        private void DoWork(object state)
        {

            Program.detectionWorkers.ForEach(x =>
            {

                if (x.AlarmActive)
                {
                    if (x.LastTriggered.AddSeconds(Program.appSettings.Motion_Active_before_alarm_Seconds) < DateTime.Now)
                    {

                        mqqtService.Alarm(x.Name, false);
                        x.AlarmActive = false;
                    }
                }
               StartMonitoring(x.RaspBerryPin);
            });

            this.mqqtService.IsActive(Program.appSettings.Mqtt_Alarm_Uptime_Monitoring);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            this.logger.LogInformation("Detection worker Service is stopping.");

            this.timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this.controller.Dispose();
            this.timer?.Dispose();
        }
}

}