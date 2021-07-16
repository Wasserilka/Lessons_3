using System;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class ByTimePeriodNetworkMetricsResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }

    public class NetworkMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public long Value { get; set; }
        public long Id { get; set; }
    }
}
