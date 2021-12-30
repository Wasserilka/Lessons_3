using System.Windows;
using MetricsManagerClient.DAL.Models;
using LiveCharts;
using LiveCharts.Defaults;

namespace MetricsManagerClient
{
    public partial class MainWindow : Window
    {
        public ChartValues<ObservableValue> Values { get; set; }
        public MetricForm form { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            Values = new ChartValues<ObservableValue>();
            form = new MetricForm();
        }
    }
}
