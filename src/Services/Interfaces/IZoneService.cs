using System;
using System.Collections.Generic;
using raspberry_mqqt_motion_alarm.Models;

namespace raspberry_mqqt_motion_alarm.Services.Interfaces
{
    public interface IZoneService
    {
        IEnumerable<Zone> FindAll();
        Zone FindOne(string id);
        Guid Insert(Zone zone);
        bool Update(Zone zone);
        void Init();
    }
}
