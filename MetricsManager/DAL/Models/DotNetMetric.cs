using System;
using Core;

namespace MetricsManager.DAL
{
    public class DotNetMetric : IMetric
    {
        public long Id { get; set; }

        public long Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }

    public class DotNetMetricAgentId : IMetricAgentId
    {
        public long Id { get; set; }

        public long Value { get; set; }

        public DateTimeOffset Time { get; set; }

        public long agentid { get; set; }
    }
}
