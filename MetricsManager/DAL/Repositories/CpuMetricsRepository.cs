using System;
using System.Collections.Generic;
using Dapper;
using Core;

namespace MetricsManager.DAL
{
    public interface ICpuMetricsRepository : IManagerRepository<CpuMetric, CpuMetricAgentId>
    {

    }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {

        public CpuMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public IList<CpuMetricAgentId> GetFromAllClusterByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                return connection.Query<CpuMetricAgentId>("SELECT * FROM cpumetrics WHERE time BETWEEN @fromTime AND @toTime",
                    new { fromTime = fromTime.ToUnixTimeSeconds(), toTime = toTime.ToUnixTimeSeconds() }).AsList();
            }
        }

        public IList<CpuMetric> GetFromAgentByTimePeriod(long agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                return connection.Query<CpuMetric>("SELECT Id, Time, Value FROM cpumetrics WHERE (time BETWEEN @fromTime AND @toTime) AND AgentId=@Id",
                    new { fromTime = fromTime.ToUnixTimeSeconds(), toTime = toTime.ToUnixTimeSeconds(), id = agentId }).AsList();
            }
        }

        public IMetric GetLast(long agentId)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                return connection.QueryFirstOrDefault<CpuMetric>("SELECT Time FROM cpumetrics WHERE Time=(SELECT MAX(Time) FROM (SELECT Time FROM cpumetrics WHERE AgentId=@Id)) AND AgentId=@Id",
                    new { Id = agentId });
            }
        }

        public void Create(IMetric metric, long agentId)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                connection.Query<CpuMetric>("INSERT INTO cpumetrics(value, time, agentId) VALUES(@value, @time, @id)",
                    new { value = metric.Value, time = metric.Time.ToUnixTimeSeconds(), id = agentId });
            }
        }
    }
}
