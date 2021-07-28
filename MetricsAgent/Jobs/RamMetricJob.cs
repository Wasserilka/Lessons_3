using MetricsAgent.DAL;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob : IJob
    {
        private IRamMetricsRepository _repository;
        private PerformanceCounter _metricCounter;

        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _metricCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var metricValue = Convert.ToInt64(_metricCounter.NextValue());
            var metricTime = DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new RamMetric { Time = metricTime, Value = metricValue });

            return Task.CompletedTask;
        }
    }
}
