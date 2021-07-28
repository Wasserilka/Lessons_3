using System;

namespace MetricsManagerClient.DAL.Models
{
    public class MetricForm
    {
        public string metric { get; set; }
        public string toDate { get; set; }
        public string toTime { get; set; }
        public string fromDate { get; set; }
        public string fromTime { get; set; }
        public string agentId { get; set; }

        public MetricForm()
        {
            metric = "Cpu";
            toDate = DateTime.Now.ToString("yyyy-MM-dd");
            toTime = DateTime.Now.ToString("HH:mm:ss");
            fromDate = "1970-01-01";
            fromTime = "00:00:00";
            agentId = "51684";
        }
    }
}
