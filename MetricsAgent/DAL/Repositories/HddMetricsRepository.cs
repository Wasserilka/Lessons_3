using System;
using System.Collections.Generic;
using Dapper;
using Core;

namespace MetricsAgent.DAL
{
    public interface IHddMetricsRepository : IRepository<HddMetric>
    {

    }

    public class HddMetricsRepository : IHddMetricsRepository
    {
        ConnectionManager _connectionManager;

        public HddMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
            _connectionManager = new ConnectionManager();
        }

        public IList<HddMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = _connectionManager.GetOpenedConnection())
            {
                return connection.Query<HddMetric>("SELECT Id, Time, Value FROM hddmetrics WHERE time BETWEEN @fromTime AND @toTime",
                    new { fromTime = fromTime.ToUnixTimeSeconds(), toTime = toTime.ToUnixTimeSeconds() }).AsList();
            }
        }

        public void Create(IMetric metric)
        {
            using (var connection = _connectionManager.GetOpenedConnection())
            {
                connection.Query<HddMetric>("INSERT INTO hddmetrics(value, time) VALUES(@value, @time)",
                    new { value = metric.Value, time = metric.Time.ToUnixTimeSeconds() });
            }
        }
    }
}
