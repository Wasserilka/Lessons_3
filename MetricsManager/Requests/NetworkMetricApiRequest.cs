using System;

namespace MetricsManager.Requests
{
    public class NetworkMetricApiRequest
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
        public string AgentUrl { get; set; }
    }
}
