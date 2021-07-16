using System;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class ByTimePeriodDotNetMetricsResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; }
    }

    public class DotNetMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public long Value { get; set; }
        public long Id { get; set; }
    }
}
