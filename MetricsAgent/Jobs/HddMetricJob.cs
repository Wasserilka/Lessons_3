using MetricsAgent.DAL;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob : IJob
    {
        private IHddMetricsRepository _repository;
        private PerformanceCounter _metricCounter;

        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _metricCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var metricValue = Convert.ToInt64(_metricCounter.NextValue());
            var metricTime = DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new HddMetric { Time = metricTime, Value = metricValue });

            return Task.CompletedTask;
        }
    }
}
