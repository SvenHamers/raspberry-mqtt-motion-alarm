using System;
using System.Threading;
using raspberry_mqqt_motion_alarm.Factories;
using raspberry_mqqt_motion_alarm.Services.Interfaces;

namespace raspberry_mqqt_motion_alarm.Services.Implementations
{
    public class MqqtAlarmService : MqqtFactory, IMqqtAlarmService
    {
        private readonly  string activeMessage;
        private readonly string inActiveMessage;
        
        public MqqtAlarmService(
            string host,
            int port,
            string userName,
            string password,
            string activeMessage,
            string inActiveMessage
            ) : base(host,port,userName,password)
        {
            this.activeMessage = activeMessage;
            this.inActiveMessage = inActiveMessage;
        }

        public async void Alarm(string zone, bool active = true)
        {
            string payload = active ? activeMessage : inActiveMessage;

            var message = MessageBuilder(zone, payload);
            await Client.PublishAsync(message, CancellationToken.None);
        }

        public async void IsActive(string aliveTopic)
        {
            string payload = $"{DateTime.UtcNow}";

            var message = MessageBuilder(aliveTopic, payload);
            await Client.PublishAsync(message,CancellationToken.None);
        }

        
    }
}
