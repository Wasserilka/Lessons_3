using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MetricsManager.DAL;
using MetricsManager.Responses;
using AutoMapper;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {

        private INetworkMetricsRepository _repository;
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly IMapper _mapper;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, INetworkMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в NetworkMetricsController");
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] long agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            var metrics = _repository.GetFromAgentByTimePeriod(agentId, fromTime, toTime);

            var response = new ByTimePeriodNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }

            _logger.LogInformation($"fromAgent: {agentId}, fromTime: {fromTime}, toTime: {toTime}");
            return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            var metrics = _repository.GetFromAllClusterByTimePeriod(fromTime, toTime);

            var response = new ByTimePeriodNetworkMetricsAgentIdResponse()
            {
                Metrics = new List<NetworkMetricAgentIdDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricAgentIdDto>(metric));
            }

            _logger.LogInformation($"from AllCluster, fromTime: {fromTime}, toTime: {toTime}");
            return Ok(response);
        }
    }
}
