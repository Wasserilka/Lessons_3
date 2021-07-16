using System;
using System.Collections.Generic;
using Dapper;
using Core;

namespace MetricsManager.DAL
{
    public interface IRamMetricsRepository : IManagerRepository<RamMetric, RamMetricAgentId>
    {

    }

    public class RamMetricsRepository : IRamMetricsRepository
    {

        public RamMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public IList<RamMetricAgentId> GetFromAllClusterByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                return connection.Query<RamMetricAgentId>("SELECT Id, Time, Value, AgentId FROM rammetrics WHERE time BETWEEN @fromTime AND @toTime",
                    new { fromTime = fromTime.ToUnixTimeSeconds(), toTime = toTime.ToUnixTimeSeconds() }).AsList();
            }
        }

        public IList<RamMetric> GetFromAgentByTimePeriod(long agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                return connection.Query<RamMetric>("SELECT Id, Time, Value FROM rammetrics WHERE (time BETWEEN @fromTime AND @toTime) AND AgentId=@Id",
                    new { fromTime = fromTime.ToUnixTimeSeconds(), toTime = toTime.ToUnixTimeSeconds(), id = agentId }).AsList();
            }
        }

        public IMetric GetLast(long agentID)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                return connection.QueryFirstOrDefault<RamMetric>("SELECT Time FROM rammetrics WHERE Time=(SELECT MAX(Time) FROM (SELECT Time FROM rammetrics WHERE AgentId=@Id)) AND AgentId=@Id",
                    new { Id = agentID });
            }
        }

        public void Create(IMetric metric, long agentId)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                connection.Query<RamMetric>("INSERT INTO rammetrics(value, time, agentId) VALUES(@value, @time, @id)",
                    new { value = metric.Value, time = metric.Time.ToUnixTimeSeconds(), id = agentId });
            }
        }
    }
}
