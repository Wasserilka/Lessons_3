using System;
using System.Collections.Generic;
using Dapper;
using Core;

namespace MetricsAgent.DAL
{
    public interface IHddMetricsRepository : IAgentRepository<HddMetric>
    {

    }

    public class HddMetricsRepository : IHddMetricsRepository
    {

        public HddMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public IList<HddMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                return connection.Query<HddMetric>("SELECT Id, Time, Value FROM hddmetrics WHERE time BETWEEN @fromTime AND @toTime",
                    new { fromTime = fromTime.ToUnixTimeSeconds(), toTime = toTime.ToUnixTimeSeconds() }).AsList();
            }
        }

        public void Create(IMetric metric)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                connection.Query<HddMetric>("INSERT INTO hddmetrics(value, time) VALUES(@value, @time)",
                    new { value = metric.Value, time = metric.Time.ToUnixTimeSeconds() });
            }
        }
    }
}
