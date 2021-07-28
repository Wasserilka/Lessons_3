using MetricsManager.DAL;
using MetricsManager.Requests;
using MetricsManager.Client;
using Quartz;
using System;
using System.Threading.Tasks;
using AutoMapper;

namespace MetricsManager.Jobs
{
    [DisallowConcurrentExecution]
    public class NetworkMetricJob : IJob
    {
        private INetworkMetricsRepository _repository;
        private IAgentsRepository _agentsRepository;
        private IMetricsAgentClient _agentClient;
        private readonly IMapper _mapper;

        public NetworkMetricJob(INetworkMetricsRepository repository, IAgentsRepository agentsRepository,
            IMetricsAgentClient agentClient, IMapper mapper)
        {
            _repository = repository;
            _agentsRepository = agentsRepository;
            _agentClient = agentClient;
            _mapper = mapper;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var agents = _agentsRepository.GetEnabledAgents();
            foreach (AgentInfo agent in agents)
            {
                var lastMetric = _repository.GetLast(agent.AgentId);
                var fromTime = lastMetric == null ?
                    DateTimeOffset.FromUnixTimeSeconds(0) : lastMetric.Time.AddSeconds(1);
                var toTime = DateTimeOffset.UtcNow;

                var metricResponse = _agentClient.GetNetworkMetrics(
                    new NetworkMetricApiRequest { FromTime = fromTime, ToTime = toTime, AgentUrl = agent.AgentUrl });

                if (metricResponse != null)
                {
                    foreach (var metric in metricResponse.metrics)
                    {
                        _repository.Create(_mapper.Map<NetworkMetric>(metric), agent.AgentId);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
