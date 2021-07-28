using MetricsManager.Responses;
using MetricsManager.Requests;
using MetricsManager.DAL;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;

namespace MetricsManager.Client
{
    class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public MetricsAgentClient(HttpClient httpClient, ILogger<MetricsAgentClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public CpuMetricsApiResponse GetCpuMetrics(CpuMetricApiRequest request)
        {
            var fromTime = request.FromTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var toTime = request.ToTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.AgentUrl}/api/metrics/cpu/from/{fromTime}/to/{toTime}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                HttpResponseMessage responseMessage = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<CpuMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public DotNetMetricsApiResponse GetDotNetMetrics(DotNetMetricApiRequest request)
        {
            var fromTime = request.FromTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var toTime = request.ToTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.AgentUrl}/api/metrics/dotnet/from/{fromTime}/to/{toTime}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<DotNetMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public HddMetricsApiResponse GetHddMetrics(HddMetricApiRequest request)
        {
            var fromTime = request.FromTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var toTime = request.ToTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.AgentUrl}/api/metrics/hdd/from/{fromTime}/to/{toTime}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<HddMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public NetworkMetricsApiResponse GetNetworkMetrics(NetworkMetricApiRequest request)
        {
            var fromTime = request.FromTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var toTime = request.ToTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.AgentUrl}/api/metrics/network/from/{fromTime}/to/{toTime}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<NetworkMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public RamMetricsApiResponse GetRamMetrics(RamMetricApiRequest request)
        {
            var fromTime = request.FromTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var toTime = request.ToTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.AgentUrl}/api/metrics/ram/from/{fromTime}/to/{toTime}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<RamMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }
    }
}
