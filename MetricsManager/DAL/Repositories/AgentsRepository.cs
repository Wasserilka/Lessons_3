using System.Collections.Generic;
using System;
using Dapper;
using Core;

namespace MetricsManager.DAL
{
    public interface IAgentsRepository
    {
        IList<AgentInfo> GetEnabledAgents();
        void Create(AgentInfo agentInfo);
        void Enable(long agentId);
        void Disable(long agentId);
    }

    public class AgentsRepository : IAgentsRepository
    {
        ConnectionManager _connectionManager;

        public AgentsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
            _connectionManager = new ConnectionManager();
        }

        public IList<AgentInfo> GetEnabledAgents()
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                return connection.Query<AgentInfo>("SELECT AgentId, AgentUrl FROM agents WHERE isenabled=@boolValue",
                    new { boolValue = 1 }).AsList();
            }
        }

        public void Create(AgentInfo agentInfo)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                connection.Query<AgentInfo>("INSERT INTO agents(AgentId, AgentUrl, IsEnabled) VALUES(@id, @url, @isenabled)",
                    new { id = agentInfo.AgentId, url = agentInfo.AgentUrl, isenabled = 0 });
            }
        }

        public void Enable(long agentId)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                connection.Query<AgentInfo>("UPDATE agents SET IsEnabled=1 WHERE AgentId=@id",
                    new { id = agentId });
            }
        }

        public void Disable(long agentId)
        {
            using (var connection = new ConnectionManager().GetOpenedConnection())
            {
                connection.Query<AgentInfo>("UPDATE agents SET IsEnabled=0 WHERE AgentId=@id",
                    new { id = agentId });
            }
        }
    }
}
