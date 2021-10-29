namespace raspberry_mqqt_motion_alarm.Services.Interfaces
{
    public interface IMqqtAlarmService
    {
        void Alarm(string zone, bool active = true);

        void IsActive(string aliveTopic);
    }
}
