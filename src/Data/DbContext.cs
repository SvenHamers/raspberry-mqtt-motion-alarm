using LiteDB;
namespace raspberry_mqqt_motion_alarm.Data
{
    public interface ILiteDbContext
    {
        LiteDatabase Database { get; }
    }

    public class LiteDbContext : ILiteDbContext
    {
        public LiteDatabase Database { get; }

        public LiteDbContext()
        {
            Database = new LiteDatabase("db.json");
        }
    }
}



