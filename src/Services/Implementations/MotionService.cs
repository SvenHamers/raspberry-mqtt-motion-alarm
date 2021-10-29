using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using raspberry_mqqt_motion_alarm.Data;
using raspberry_mqqt_motion_alarm.Models;
using raspberry_mqqt_motion_alarm.Services.Interfaces;

namespace raspberry_mqqt_motion_alarm.Services.Implementations
{


    public class MotionService : IMotionService
    {
        private LiteDatabase _liteDb;
        private const string _document = "zones";

        public MotionService(ILiteDbContext liteDbContext)
        {
            _liteDb = liteDbContext.Database;

        }

        public IEnumerable<MotionDetector> FindAll(string zone)
        {
            var result = _liteDb.GetCollection<Zone>(_document)
                .Find(x => x.Name == zone).FirstOrDefault().MotionDetectors;
            return result;
        }

        public MotionDetector FindOne(string zone, Guid motionDetectorId)
        {
            return _liteDb.GetCollection<Zone>(_document)
                .Find(x => x.Name == zone).FirstOrDefault().MotionDetectors.FirstOrDefault(x => x.Id == motionDetectorId);
        }

        public Guid Insert(string zone, MotionDetector forecast)
        {

            forecast.Id = Guid.NewGuid();

            var result = _liteDb.GetCollection<Zone>(_document)
                .Find(x => x.Name == zone).FirstOrDefault();


            result.MotionDetectors.Add(forecast);

            Update(result);
            return forecast.Id;
        }

        public bool Update(Zone forecast)
        {
            return _liteDb.GetCollection<Zone>(_document)
                .Update(forecast);
        }
    }
}
