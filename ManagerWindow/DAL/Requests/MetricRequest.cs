namespace MetricsManagerClient.DAL.Requests
{
    class MetricRequest
    {
        public string AgnetId { get; set; }
        public string Metric { get; set; }
        public string fromDateTime { get; set; }
        public string toDateTime { get; set; }
    }
}
