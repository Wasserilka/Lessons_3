using System;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class ByTimePeriodRamMetricsResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }

    public class RamMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public long Value { get; set; }
        public long Id { get; set; }
    }
}
