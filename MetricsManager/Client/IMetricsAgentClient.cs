using MetricsManager.Responses;
using MetricsManager.Requests;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        CpuMetricsApiResponse GetCpuMetrics(CpuMetricApiRequest request);
        DotNetMetricsApiResponse GetDotNetMetrics(DotNetMetricApiRequest request);
        HddMetricsApiResponse GetHddMetrics(HddMetricApiRequest request);
        NetworkMetricsApiResponse GetNetworkMetrics(NetworkMetricApiRequest request);
        RamMetricsApiResponse GetRamMetrics(RamMetricApiRequest request);
    }
}
