using System;
using LiteDB;

namespace raspberry_mqqt_motion_alarm.Models
{
    public class MotionDetector
    {

        public MotionDetector()
        {
            
        }

        public MotionDetector(string name,string location)
        {
            this.Name = name;
            this.Location = location;
        }


        public Guid Id { get;  set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int RaspBerryPin { get; set; }

        [BsonIgnore]
        public DateTime LastTriggered { get; set; }
        [BsonIgnore]
        public bool AlarmActive { get; set; }
    }
}
