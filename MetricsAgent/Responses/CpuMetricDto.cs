using System;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class ByTimePeriodCpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
    public class CpuMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public long Value { get; set; }
        public long Id { get; set; }
    }
}
