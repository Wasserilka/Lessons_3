using System;

namespace MetricsAgent.DAL
{
    public class RamMetric
    {
        public long Id { get; set; }

        public long Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
