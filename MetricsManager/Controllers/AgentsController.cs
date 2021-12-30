using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MetricsManager.DAL;
using MetricsManager.Responses;
using AutoMapper;

namespace MetricsManager.Controllers
{
    [Route("api/agents")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private IAgentsRepository _repository;
        private readonly ILogger<AgentsController> _logger;
        private readonly IMapper _mapper;

        public AgentsController(ILogger<AgentsController> logger, IAgentsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в AgentsController");
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// Регистрация нового агента в базе данных
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/agents/create?agentId=51684&amp;agentUrl=http://localhost:51684
        ///
        /// </remarks>
        /// <param name="agentId">Идентификационный номер агента</param>
        /// <param name="agentUrl">Адресс агента</param>
        /// <response code="200">Успешный запрос</response>
        /// <response code="400">Переданы неверные данные</response>  
        [HttpPost("create")]
        public IActionResult CreateAgent([FromQuery] long agentId, [FromQuery] string agentUrl)
        {
            _repository.Create(new AgentInfo { AgentId = agentId, AgentUrl = agentUrl });

            _logger.LogInformation($"created agent id={agentId}, url={agentUrl}");
            return Ok();
        }

        /// <summary>
        /// Активация указанного агента
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/agents/enable/51684
        ///
        /// </remarks>
        /// <param name="agentId">Идентификационный номер агента</param>
        /// <response code="200">Успешный запрос</response>
        /// <response code="400">Переданы неверные данные</response>  
        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] long agentId)
        {
            _repository.Enable(agentId);

            _logger.LogInformation($"enabled agent id={agentId}");
            return Ok();
        }

        /// <summary>
        /// Блокировка указанного агента
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/agents/disable/51684
        ///
        /// </remarks>
        /// <param name="agentId">Идентификационный номер агента</param>
        /// <response code="200">Успешный запрос</response>
        /// <response code="400">Переданы неверные данные</response>  
        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] long agentId)
        {
            _repository.Disable(agentId);

            _logger.LogInformation($"disabled agent id={agentId}");
            return Ok();
        }

        /// <summary>
        /// Получает cписок всех агентов
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET api/agents/list
        ///
        /// </remarks>
        /// <returns>Список всех агентов</returns>
        /// <response code="200">Успешный запрос</response>
        /// <response code="400">Переданы неверные данные</response>  
        [HttpGet("list")]
        public IActionResult GetAgentsList()
        {
            var agents = _repository.GetEnabledAgents();

            var response = new AgentsListResponse()
            {
                Agents = new List<AgentInfoDto>()
            };

            foreach (var agent in agents)
            {
                response.Agents.Add(_mapper.Map<AgentInfoDto>(agent));
            }

            _logger.LogInformation("GetAgentsList");
            return Ok(response);
        }
    }
}
