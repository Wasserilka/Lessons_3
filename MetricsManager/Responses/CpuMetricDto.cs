using System;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class ByTimePeriodCpuMetricsAgentIdResponse
    {
        public List<CpuMetricAgentIdDto> Metrics { get; set; }
    }

    public class ByTimePeriodCpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }

    public class CpuMetricsApiResponse
    {
        public List<CpuMetricDto> metrics { get; set; }
    }

    public class CpuMetricDto
    {
        public DateTimeOffset time { get; set; }
        public long value { get; set; }
        public long id { get; set; }
    }

    public class CpuMetricAgentIdDto
    {
        public DateTimeOffset time { get; set; }
        public long value { get; set; }
        public long id { get; set; }
        public long agentid { get; set; }
    }
}
