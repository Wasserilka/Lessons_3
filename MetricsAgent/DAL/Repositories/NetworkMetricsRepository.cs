using System;
using System.Collections.Generic;
using Dapper;
using Core;

namespace MetricsAgent.DAL
{
    public interface INetworkMetricsRepository : IAgentRepository<NetworkMetric>
    {

    }

    public class NetworkMetricsRepository : INetworkMetricsRepository
    {

        public NetworkMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public IList<NetworkMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                return connection.Query<NetworkMetric>("SELECT Id, Time, Value FROM networkmetrics WHERE time BETWEEN @fromTime AND @toTime",
                    new { fromTime = fromTime.ToUnixTimeSeconds(), toTime = toTime.ToUnixTimeSeconds() }).AsList();
            }
        }

        public void Create(IMetric metric)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                connection.Query<NetworkMetric>("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
                    new { value = metric.Value, time = metric.Time.ToUnixTimeSeconds() });
            }
        }
    }
}
