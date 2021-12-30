using System;
using System.Windows;
using System.Net.Http;
using System.Text.Json;
using MetricsManagerClient.DAL.Requests;
using MetricsManagerClient.DAL.Responses;

namespace MetricsManagerClient.DAL.Models
{
    static class ConnectionClient
    {
        static public MetricResponse GetMetricsFromAgent(MetricRequest request)
        {
            var httpRequest = new HttpRequestMessage(
                HttpMethod.Get, 
                $"http://localhost:50684/api/metrics/{request.Metric}/agent/{request.AgnetId}/from/{request.fromDateTime}/to/{request.toDateTime}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                HttpResponseMessage response = new HttpClient().SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<MetricResponse>(responseStream, options).Result;
            }
            catch (Exception)
            {
                MessageBox.Show("Нет ответа от сервера.");
            }

            return null;
        }
    }
}
