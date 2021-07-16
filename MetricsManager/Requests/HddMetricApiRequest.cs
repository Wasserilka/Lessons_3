using System;

namespace MetricsManager.Requests
{
    public class HddMetricApiRequest
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
        public string AgentUrl { get; set; }
    }
}
