using System;

namespace Core
{
    public interface IMetricAgentId
    {
        public long Id { get; set; }

        public long Value { get; set; }

        public DateTimeOffset Time { get; set; }

        public long agentid { get; set; }
    }
}
