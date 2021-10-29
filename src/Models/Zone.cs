using System;
using System.Collections.Generic;
using LiteDB;

namespace raspberry_mqqt_motion_alarm.Models
{
    public class Zone
    {
        public Zone()
        {
            this.MotionDetectors = new List<MotionDetector>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Location { get; set; }
        public List<MotionDetector> MotionDetectors { get; set; }

    }
}
