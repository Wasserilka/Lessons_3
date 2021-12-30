using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MetricsManager.DAL;
using MetricsManager.Responses;
using AutoMapper;


namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {

        private IHddMetricsRepository _repository;
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IMapper _mapper;

        public HddMetricsController(ILogger<HddMetricsController> logger, IHddMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в HddMetricsController");
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// Получает метрики HDD от указанного агента на заданном диапазоне времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/metrics/hdd/agent/51684/from/1970-01-01T00:00:00Z/to/1970-01-02T00:00:00Z
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

            var response = new ByTimePeriodHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
            }

            _logger.LogInformation($"fromAgent: {agentId}, fromTime: {fromTime}, toTime: {toTime}");
            return Ok(response);
        }

        /// <summary>
        /// Получает метрики HDD от всех агентов на заданном диапазоне времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/metrics/hdd/cluster/from/1970-01-01T00:00:00Z/to/1970-01-02T00:00:00Z
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

            var response = new ByTimePeriodHddMetricsAgentIdResponse()
            {
                Metrics = new List<HddMetricAgentIdDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricAgentIdDto>(metric));
            }

            _logger.LogInformation($"from AllCluster, fromTime: {fromTime}, toTime: {toTime}");
            return Ok(response);
        }
    }
}
