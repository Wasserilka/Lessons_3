using MetricsAgent.DAL;
using MetricsAgent.Responses;
using MetricsAgent.Requests;
using AutoMapper;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricDto>();
            CreateMap<CpuMetricCreateRequest, CpuMetric>();

            CreateMap<DotNetMetric, DotNetMetricDto>();
            CreateMap<DotNetMetricCreateRequest, DotNetMetric>();

            CreateMap<HddMetric, HddMetricDto>();
            CreateMap<HddMetricCreateRequest, HddMetric>();

            CreateMap<NetworkMetric, NetworkMetricDto>();
            CreateMap<NetworkMetricCreateRequest, NetworkMetric>();

            CreateMap<RamMetric, RamMetricDto>();
            CreateMap<RamMetricCreateRequest, RamMetric>();
        }
    }
}
