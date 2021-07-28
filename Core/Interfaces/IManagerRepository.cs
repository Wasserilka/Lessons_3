using System;
using System.Collections.Generic;

namespace Core
{
    public interface IManagerRepository<T, U> 
        where T : class
        where U : class
    {
        IList<U> GetFromAllClusterByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime);
        IList<T> GetFromAgentByTimePeriod(long agentId, DateTimeOffset fromTime, DateTimeOffset toTime);
        void Create(IMetric metric, long agentId);
        IMetric GetLast(long agentId);
    }
}
