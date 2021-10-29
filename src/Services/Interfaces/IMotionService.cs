using System;
using System.Collections.Generic;
using raspberry_mqqt_motion_alarm.Models;

namespace raspberry_mqqt_motion_alarm.Services.Interfaces
{
    public interface IMotionService
    {
        IEnumerable<MotionDetector> FindAll(string zone);
        MotionDetector FindOne(string id, Guid motionDetectorId);
        Guid Insert(string zone, MotionDetector detector);
        bool Update(Zone zone);
    }
}
