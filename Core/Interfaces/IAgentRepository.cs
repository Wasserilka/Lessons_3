using System.Collections.Generic;
using System;

namespace Core
{
    public interface IAgentRepository<T> where T : class
    {
        IList<T> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime);
        void Create(IMetric metric);
    }
}
