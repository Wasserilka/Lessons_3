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

        [HttpPost("create")]
        public IActionResult CreateAgent([FromQuery] long agentId, [FromQuery] string agentUrl)
        {
            _repository.Create(new AgentInfo { AgentId = agentId, AgentUrl = agentUrl });

            _logger.LogInformation($"created agent id={agentId}, url={agentUrl}");
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] long agentId)
        {
            _repository.Enable(agentId);

            _logger.LogInformation($"enabled agent id={agentId}");
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] long agentId)
        {
            _repository.Disable(agentId);

            _logger.LogInformation($"disabled agent id={agentId}");
            return Ok();
        }

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
