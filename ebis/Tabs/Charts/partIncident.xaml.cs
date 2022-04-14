using Data;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Ebis.Tabs.Charts
{
    public partial class PartIncident : UserControl
    {
        private MongoDatabase mongoDatabase;
       
        public PartIncident()
        {
            InitializeComponent();
            mongoDatabase = new MongoDatabase();

            Dictionary<string, double> dataPieChart = mongoDatabase.StatPartNiveauIncident();

            chartImportant.Values.Add(dataPieChart["Important"]);
            chartCritique.Values.Add(dataPieChart["Critique"]);
            chartFaible.Values.Add(dataPieChart["Faible"]);

            DataContext = this;
        }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (PieChart)chartpoint.ChartView;
           
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }
    }
}
