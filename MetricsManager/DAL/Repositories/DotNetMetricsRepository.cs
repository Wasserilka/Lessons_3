using System;
using System.Collections.Generic;
using Dapper;
using Core;

namespace MetricsManager.DAL
{
    public interface IDotNetMetricsRepository : IManagerRepository<DotNetMetric, DotNetMetricAgentId>
    {

    }

    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {

        public DotNetMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public IList<DotNetMetricAgentId> GetFromAllClusterByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                return connection.Query<DotNetMetricAgentId>("SELECT Id, Time, Value, AgentId FROM dotnetmetrics WHERE time BETWEEN @fromTime AND @toTime",
                    new { fromTime = fromTime.ToUnixTimeSeconds(), toTime = toTime.ToUnixTimeSeconds() }).AsList();
            }
        }

        public IList<DotNetMetric> GetFromAgentByTimePeriod(long agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                return connection.Query<DotNetMetric>("SELECT Id, Time, Value FROM dotnetmetrics WHERE (time BETWEEN @fromTime AND @toTime) AND AgentId=@Id",
                    new { fromTime = fromTime.ToUnixTimeSeconds(), toTime = toTime.ToUnixTimeSeconds(), id = agentId }).AsList();
            }
        }

        public IMetric GetLast(long agentId)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                return connection.QueryFirstOrDefault<DotNetMetric>("SELECT Time FROM dotnetmetrics WHERE Time=(SELECT MAX(Time) FROM (SELECT Time FROM dotnetmetrics WHERE AgentId=@Id)) AND AgentId=@Id",
                    new { Id = agentId });
            }
        }

        public void Create(IMetric metric, long agentId)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                connection.Query<DotNetMetric>("INSERT INTO dotnetmetrics(value, time, agentId) VALUES(@value, @time, @id)",
                    new { value = metric.Value, time = metric.Time.ToUnixTimeSeconds(), id = agentId });
            }
        }
    }
}
