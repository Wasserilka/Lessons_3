using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MetricsManager.DAL;
using MetricsManager.Responses;
using AutoMapper;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {

        private IDotNetMetricsRepository _repository;
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IMapper _mapper;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IDotNetMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в DotNetMetricsController");
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// Получает метрики DotNet от указанного агента на заданном диапазоне времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/metrics/dotnet/agent/51684/from/1970-01-01T00:00:00Z/to/1970-01-02T00:00:00Z
        ///
        /// </remarks>
        /// <param name="agentId">Идентификационный номер агента</param>
        /// <param name="fromTime">Начальная метка времени в секундах с 01.01.1970</param>
        /// <param name="toTime">Конечная метка времени в секундах с 01.01.1970</param>
        /// <returns>Список метрик от указанного агента, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="200">Успешный запрос</response>
        /// <response code="400">Переданы неверные данные</response>  
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] long agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            var metrics = _repository.GetFromAgentByTimePeriod(agentId, fromTime, toTime);

            var response = new ByTimePeriodDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
            }

            _logger.LogInformation($"fromAgent: {agentId}, fromTime: {fromTime}, toTime: {toTime}");
            return Ok(response);
        }

        /// <summary>
        /// Получает метрики DotNet от всех агентов на заданном диапазоне времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/metrics/dotnet/cluster/from/1970-01-01T00:00:00Z/to/1970-01-02T00:00:00Z
        ///
        /// </remarks>
        /// <param name="fromTime">Начальная метка времени в секундах с 01.01.1970</param>
        /// <param name="toTime">Конечная метка времени в секундах с 01.01.1970</param>
        /// <returns>Список метрик от всех агентов, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="200">Успешный запрос</response>
        /// <response code="400">Переданы неверные данные</response>  
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            var metrics = _repository.GetFromAllClusterByTimePeriod(fromTime, toTime);

            var response = new ByTimePeriodDotNetMetricsAgentIdResponse()
            {
                Metrics = new List<DotNetMetricAgentIdDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricAgentIdDto>(metric));
            }

            _logger.LogInformation($"from AllCluster, fromTime: {fromTime}, toTime: {toTime}");
            return Ok(response);
        }
    }
}
