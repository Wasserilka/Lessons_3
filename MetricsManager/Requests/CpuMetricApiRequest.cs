﻿using System;

namespace MetricsManager.Requests
{
    public class CpuMetricApiRequest
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
        public string AgentUrl { get; set; }
    }
}
