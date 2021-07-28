using MetricsAgent.DAL;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    [DisallowConcurrentExecution]
    public class DotNetMetricJob : IJob
    {
        private IDotNetMetricsRepository _repository;

        public DotNetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            long metricValue = 0;
            foreach (string instance in new PerformanceCounterCategory(".NET CLR Memory").GetInstanceNames())
            {
                try
                {
                    metricValue += Convert.ToInt64(new PerformanceCounter(".NET CLR Memory", "# bytes in all heaps", instance).NextValue());
                }
                catch (Exception)
                {
                }
            }
            var metricTime = DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new DotNetMetric { Time = metricTime, Value = metricValue });

            return Task.CompletedTask;
        }
    }
}
