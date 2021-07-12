using System;

namespace MetricsAgent.DAL
{
    public class CpuMetric
    {
        public long Id { get; set; }

        public long Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
