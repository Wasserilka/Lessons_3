using System;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class ByTimePeriodHddMetricsAgentIdResponse
    {
        public List<HddMetricAgentIdDto> Metrics { get; set; }
    }

    public class ByTimePeriodHddMetricsResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }
    public class HddMetricsApiResponse
    {
        public List<HddMetricDto> metrics { get; set; }
    }

    public class HddMetricDto
    {
        public DateTimeOffset time { get; set; }
        public long value { get; set; }
        public long id { get; set; }
    }

    public class HddMetricAgentIdDto
    {
        public DateTimeOffset time { get; set; }
        public long value { get; set; }
        public long id { get; set; }
        public long agentid { get; set; }
    }
}
