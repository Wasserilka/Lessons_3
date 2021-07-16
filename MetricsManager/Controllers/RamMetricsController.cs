using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MetricsManager.DAL;
using MetricsManager.Responses;
using AutoMapper;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {

        private IRamMetricsRepository _repository;
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IMapper _mapper;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRamMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamMetricsController");
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// Получает метрики RAM от указанного агента на заданном диапазоне времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/metrics/ram/agent/51684/from/1970-01-01T00:00:00Z/to/1970-01-02T00:00:00Z
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

            var response = new ByTimePeriodRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
            }

            _logger.LogInformation($"fromAgent: {agentId}, fromTime: {fromTime}, toTime: {toTime}");
            return Ok(response);
        }

        /// <summary>
        /// Получает метрики RAM от всех агентов на заданном диапазоне времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/metrics/ram/cluster/from/1970-01-01T00:00:00Z/to/1970-01-02T00:00:00Z
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

            var response = new ByTimePeriodRamMetricsAgentIdResponse()
            {
                Metrics = new List<RamMetricAgentIdDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricAgentIdDto>(metric));
            }

            _logger.LogInformation($"from AllCluster, fromTime: {fromTime}, toTime: {toTime}");
            return Ok(response);
        }
    }
}
