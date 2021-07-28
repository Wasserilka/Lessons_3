using System;
using System.Collections.Generic;
using Dapper;
using Core;

namespace MetricsManager.DAL
{
    public interface IHddMetricsRepository : IManagerRepository<HddMetric, HddMetricAgentId>
    {

    }

    public class HddMetricsRepository : IHddMetricsRepository
    {

        public HddMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public IList<HddMetricAgentId> GetFromAllClusterByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                return connection.Query<HddMetricAgentId>("SELECT Id, Time, Value, AgentId FROM hddmetrics WHERE time BETWEEN @fromTime AND @toTime",
                    new { fromTime = fromTime.ToUnixTimeSeconds(), toTime = toTime.ToUnixTimeSeconds() }).AsList();
            }
        }

        public IList<HddMetric> GetFromAgentByTimePeriod(long agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                return connection.Query<HddMetric>("SELECT Id, Time, Value FROM hddmetrics WHERE (time BETWEEN @fromTime AND @toTime) AND AgentId=@Id",
                    new { fromTime = fromTime.ToUnixTimeSeconds(), toTime = toTime.ToUnixTimeSeconds(), id = agentId }).AsList();
            }
        }

        public IMetric GetLast(long agentID)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                return connection.QueryFirstOrDefault<HddMetric>("SELECT Time FROM hddmetrics WHERE Time=(SELECT MAX(Time) FROM (SELECT Time FROM hddmetrics WHERE AgentId=@Id)) AND AgentId=@Id",
                    new { Id = agentID });
            }
        }

        public void Create(IMetric metric, long agentId)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                connection.Query<HddMetric>("INSERT INTO hddmetrics(value, time, agentId) VALUES(@value, @time, @id)",
                    new { value = metric.Value, time = metric.Time.ToUnixTimeSeconds(), id = agentId });
            }
        }
    }
}
