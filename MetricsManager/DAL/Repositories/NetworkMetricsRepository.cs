using System;
using System.Collections.Generic;
using Dapper;
using Core;

namespace MetricsManager.DAL
{
    public interface INetworkMetricsRepository : IManagerRepository<NetworkMetric, NetworkMetricAgentId>
    {

    }

    public class NetworkMetricsRepository : INetworkMetricsRepository
    {

        public NetworkMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public IList<NetworkMetricAgentId> GetFromAllClusterByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                return connection.Query<NetworkMetricAgentId>("SELECT Id, Time, Value, AgentId FROM networkmetrics WHERE time BETWEEN @fromTime AND @toTime",
                    new { fromTime = fromTime.ToUnixTimeSeconds(), toTime = toTime.ToUnixTimeSeconds() }).AsList();
            }
        }

        public IList<NetworkMetric> GetFromAgentByTimePeriod(long agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                return connection.Query<NetworkMetric>("SELECT Id, Time, Value FROM networkmetrics WHERE (time BETWEEN @fromTime AND @toTime) AND AgentId=@Id",
                    new { fromTime = fromTime.ToUnixTimeSeconds(), toTime = toTime.ToUnixTimeSeconds(), id = agentId }).AsList();
            }
        }

        public IMetric GetLast(long agentID)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                return connection.QueryFirstOrDefault<NetworkMetric>("SELECT Time FROM networkmetrics WHERE Time=(SELECT MAX(Time) FROM (SELECT Time FROM networkmetrics WHERE AgentId=@Id)) AND AgentId=@Id",
                    new { Id = agentID });
            }
        }

        public void Create(IMetric metric, long agentId)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                connection.Query<NetworkMetric>("INSERT INTO networkmetrics(value, time, agentId) VALUES(@value, @time, @id)",
                    new { value = metric.Value, time = metric.Time.ToUnixTimeSeconds(), id = agentId });
            }
        }
    }
}
