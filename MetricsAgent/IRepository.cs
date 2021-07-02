using System.Collections.Generic;
using System;

namespace MetricsAgent.DAL
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime);
    }
}
