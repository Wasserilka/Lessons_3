using System;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class ByTimePeriodNetworkMetricsAgentIdResponse
    {
        public List<NetworkMetricAgentIdDto> Metrics { get; set; }
    }

    public class ByTimePeriodNetworkMetricsResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }
    public class NetworkMetricsApiResponse
    {
        public List<NetworkMetricDto> metrics { get; set; }
    }

    public class NetworkMetricDto
    {
        public DateTimeOffset time { get; set; }
        public long value { get; set; }
        public long id { get; set; }
    }

    public class NetworkMetricAgentIdDto
    {
        public DateTimeOffset time { get; set; }
        public long value { get; set; }
        public long id { get; set; }
        public long agentid { get; set; }
    }
}
