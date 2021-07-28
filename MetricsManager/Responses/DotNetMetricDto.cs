using System;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class ByTimePeriodDotNetMetricsAgentIdResponse
    {
        public List<DotNetMetricAgentIdDto> Metrics { get; set; }
    }

    public class ByTimePeriodDotNetMetricsResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; }
    }

    public class DotNetMetricsApiResponse
    {
        public List<DotNetMetricDto> metrics { get; set; }
    }

    public class DotNetMetricDto
    {
        public DateTimeOffset time { get; set; }
        public long value { get; set; }
        public long id { get; set; }
    }

    public class DotNetMetricAgentIdDto
    {
        public DateTimeOffset time { get; set; }
        public long value { get; set; }
        public long id { get; set; }
        public long agentid { get; set; }
    }
}
