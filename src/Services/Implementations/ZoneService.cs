using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using raspberry_mqqt_motion_alarm.Data;
using raspberry_mqqt_motion_alarm.Models;
using raspberry_mqqt_motion_alarm.Services.Interfaces;

namespace raspberry_mqqt_motion_alarm.Services.Implementations
{
    public class ZoneService : IZoneService
    {
        private LiteDatabase _liteDb;
        private const string _document = "zones";

        public ZoneService(ILiteDbContext liteDbContext)
        {
            _liteDb = liteDbContext.Database;
        }

        public void Init()
        {
            foreach (var res in FindAll())
            {
                Program.detectionWorkers.AddRange(res.MotionDetectors);
            }
        }

        public IEnumerable<Zone> FindAll()
        {
            var result = _liteDb.GetCollection<Zone>(_document)
                .FindAll();
            return result;
        }

        public Zone FindOne(string id)
        {
            return _liteDb.GetCollection<Zone>(_document)
                .Find(x => x.Name == id).FirstOrDefault();
        }

        public Guid Insert(Zone zone)
        {
            zone.Id = Guid.NewGuid();
            return _liteDb.GetCollection<Zone>(_document)
                .Insert(zone);
        }

        public bool Update(Zone zone)
        {
            return _liteDb.GetCollection<Zone>(_document)
                .Update(zone);
        }

    }
}
