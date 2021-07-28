using System.Collections.Generic;

namespace MetricsManagerClient.DAL.Responses
{
    public class MetricResponse
    {
        public List<Metric> metrics { get; set; }
    }
    public class Metric
    {
        public long value { get; set; }
    }
}
