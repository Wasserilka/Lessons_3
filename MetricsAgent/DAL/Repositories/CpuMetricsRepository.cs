using System;
using System.Collections.Generic;
using Dapper;
using Core;

namespace MetricsAgent.DAL
{
    public interface ICpuMetricsRepository : IAgentRepository<CpuMetric>
    {

    }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {

        public CpuMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public IList<CpuMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                return connection.Query<CpuMetric>("SELECT Id, Time, Value FROM cpumetrics WHERE time BETWEEN @fromTime AND @toTime",
                    new { fromTime = fromTime.ToUnixTimeSeconds(), toTime = toTime.ToUnixTimeSeconds() }).AsList();
            }
        }

        public void Create(IMetric metric)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                connection.Query<CpuMetric>("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
                    new { value = metric.Value, time = metric.Time.ToUnixTimeSeconds() });
            }
        }
    }
}
