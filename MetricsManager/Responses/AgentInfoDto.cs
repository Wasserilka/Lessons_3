using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class AgentsListResponse
    {
        public List<AgentInfoDto> Agents { get; set; }
    }
    public class AgentInfoDto
    {
        public long AgentId { get; set; }
        public string AgentUrl { get; set; }
    }
}
