using System;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class ByTimePeriodRamMetricsAgentIdResponse
    {
        public List<RamMetricAgentIdDto> Metrics { get; set; }
    }

    public class ByTimePeriodRamMetricsResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
    public class RamMetricsApiResponse
    {
        public List<RamMetricDto> metrics { get; set; }
    }

    public class RamMetricDto
    {
        public DateTimeOffset time { get; set; }
        public long value { get; set; }
        public long id { get; set; }
    }

    public class RamMetricAgentIdDto
    {
        public DateTimeOffset time { get; set; }
        public long value { get; set; }
        public long id { get; set; }
        public long agentid { get; set; }
    }
}
