using System;

namespace MetricsAgent.Requests
{
    public class HddMetricCreateRequest
    {
        public DateTimeOffset Time { get; set; }
        public long Value { get; set; }
    }
}
