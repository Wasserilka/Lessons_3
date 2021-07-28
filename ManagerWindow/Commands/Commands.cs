using System.Windows;
using System.Windows.Input;
using MetricsManagerClient.DAL.Models;
using MetricsManagerClient.DAL.Requests;
using MetricsManagerClient.DAL.Responses;
using LiveCharts;
using LiveCharts.Defaults;

namespace MetricsManagerClient.Commands
{
    class Commands
    {
        public static readonly ICommand GetMetricCommand = new RelayCommand(o =>
        {
            var container = (object[])o;

            var request = new MetricRequest 
            { 
                AgnetId = ((MetricForm)container[0]).agentId, 
                Metric = ((MetricForm)container[0]).metric,
                fromDateTime = $"{((MetricForm)container[0]).fromDate}T{((MetricForm)container[0]).fromTime}Z",
                toDateTime = $"{((MetricForm)container[0]).toDate}T{((MetricForm)container[0]).toTime}Z"
            };

            var response = ConnectionClient.GetMetricsFromAgent(request);
            if (response ==  null)
            {
                MessageBox.Show("Данные не получены.");
                return;
            }

            var values = (ChartValues<ObservableValue>)container[1];
            values.Clear();
            foreach(Metric item in response.metrics)
            {
                values.Add(new ObservableValue( item.value));
            }
        });
    }
}