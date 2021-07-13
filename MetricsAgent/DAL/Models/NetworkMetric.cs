using System;
using Core;

namespace MetricsAgent.DAL
{
    public class NetworkMetric : IMetric
    {
        public long Id { get; set; }

        public long Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
