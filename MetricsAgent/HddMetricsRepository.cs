using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.DAL
{
    public class HddMetric
    {
        public long Id { get; set; }

        public long Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }

    public interface IHddMetricsRepository : IRepository<HddMetric>
    {

    }

    public class HddMetricsRepository : IHddMetricsRepository
    {
        public IList<HddMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = IConnectionManager.GetOpenedConnection();
            using var cmd = new SQLiteCommand(connection);

            cmd.CommandText = "SELECT * FROM hddmetrics WHERE time BETWEEN @fromTime AND @toTime";
            cmd.Parameters.AddWithValue("@fromTime", fromTime.ToUnixTimeSeconds());
            cmd.Parameters.AddWithValue("@toTime", toTime.ToUnixTimeSeconds());

            var returnList = new List<HddMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new HddMetric
                    {
                        Id = reader.GetInt64(0),
                        Value = reader.GetInt64(1),
                        Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt32(2))
                    });
                }
            }

            return returnList;
        }
    }
}
