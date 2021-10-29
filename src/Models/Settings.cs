using System;
namespace raspberry_mqqt_motion_alarm.Models
{
    public class Settings
    {

        public Settings() { }

        public Settings(
            string api_Password,
            string mqtt_Host,
            string mqtt_User,
            string mqtt_Password,
            int mqtt_Port = 181,
            string api_Url = "http://*:5001",
            int motion_Active_before_alarm_Seconds = 20,
            string mqtt_Alarm_Message_Active = "active",
            string mqtt_Alarm_Message_InActive = "inactive",
            string mqtt_Alarm_Uptime_Monitoring = "motion"
            )
        {
            this.Api_Password = api_Password;
            this.Mqtt_Host = mqtt_Host;
            this.Mqtt_User = mqtt_User;
            this.Mqtt_Password = mqtt_Password;
            this.Mqtt_Port = mqtt_Port;
            this.Api_Url = api_Url;
            this.Motion_Active_before_alarm_Seconds = motion_Active_before_alarm_Seconds;
            this.Mqtt_Alarm_Message_Active = mqtt_Alarm_Message_Active;
            this.Mqtt_Alarm_Message_InActive = mqtt_Alarm_Message_InActive;
            this.Mqtt_Alarm_Uptime_Monitoring = mqtt_Alarm_Uptime_Monitoring;
        }


        public int Motion_Active_before_alarm_Seconds { get; set; }


        public string Api_Url { get; set; }
        public string Api_Password { get; set; }

        public string Mqtt_Host { get; set; }
        public int Mqtt_Port { get; set; }
        public string Mqtt_User { get; set; }
        public string Mqtt_Password { get; set; }
        public string Mqtt_Alarm_Message_Active { get; set; }
        public string Mqtt_Alarm_Message_InActive { get; set; }
        public string Mqtt_Alarm_Uptime_Monitoring { get; set; }
    }
}
