using System;

namespace MetricsAgent.Requests
{
    public class NetworkMetricCreateRequest
    {
        public DateTimeOffset Time { get; set; }
        public long Value { get; set; }
    }
}
