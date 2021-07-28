using MetricsAgent.DAL;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    [DisallowConcurrentExecution]
    public class NetworkMetricJob : IJob
    {
        private INetworkMetricsRepository _repository;
        private PerformanceCounter _metricCounterSent;
        private PerformanceCounter _metricCounterReceived;

        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            var instanceName = new PerformanceCounterCategory("Network Interface").GetInstanceNames();
            _metricCounterSent = new PerformanceCounter("Network Interface", "Bytes Sent/sec", instanceName[0]);
            _metricCounterReceived = new PerformanceCounter("Network Interface", "Bytes Received/sec", instanceName[0]);
        }

        public Task Execute(IJobExecutionContext context)
        {
            var metricValue = Convert.ToInt64(_metricCounterSent.NextValue()) + Convert.ToInt64(_metricCounterReceived.NextValue());
            var metricTime = DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new NetworkMetric { Time = metricTime, Value = metricValue });

            return Task.CompletedTask;
        }
    }
}
