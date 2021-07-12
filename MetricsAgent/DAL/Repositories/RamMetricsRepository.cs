using System;
using System.Collections.Generic;
using Dapper;
using Core;

namespace MetricsAgent.DAL
{
    public interface IRamMetricsRepository : IRepository<RamMetric>
    {

    }

    public class RamMetricsRepository : IRamMetricsRepository
    {
        public RamMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        public IList<RamMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = IConnectionManager.GetOpenedConnection())
            {
                return connection.Query<RamMetric>("SELECT Id, Time, Value FROM rammetrics WHERE time BETWEEN @fromTime AND @toTime",
                    new { fromTime = fromTime.ToUnixTimeSeconds(), toTime = toTime.ToUnixTimeSeconds() }).AsList();
            }
        }
    }
}
