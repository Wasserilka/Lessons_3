using MetricsManager.DAL;
using MetricsManager.Responses;
using AutoMapper;

namespace MetricsManager
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricDto>();
            CreateMap<CpuMetricDto, CpuMetric>();
            CreateMap<CpuMetricAgentId, CpuMetricAgentIdDto>();

            CreateMap<DotNetMetric, DotNetMetricDto>();
            CreateMap<DotNetMetricDto, DotNetMetric>();
            CreateMap<DotNetMetricAgentId, DotNetMetricAgentIdDto>();

            CreateMap<HddMetric, HddMetricDto>();
            CreateMap<HddMetricDto, HddMetric>();
            CreateMap<HddMetricAgentId, HddMetricAgentIdDto>();

            CreateMap<NetworkMetric, NetworkMetricDto>();
            CreateMap<NetworkMetricDto, NetworkMetric>();
            CreateMap<NetworkMetricAgentId, NetworkMetricAgentIdDto>();

            CreateMap<RamMetric, RamMetricDto>();
            CreateMap<RamMetricDto, RamMetric>();
            CreateMap<RamMetricAgentId, RamMetricAgentIdDto>();

            CreateMap<AgentInfo, AgentInfoDto>();
        }
    }
}
