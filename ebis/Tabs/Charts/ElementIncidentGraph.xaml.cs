using Data;
using LiveCharts;
using LiveCharts.Wpf;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Ebis.Tabs.Charts
{

    public partial class ElementIncidentGraph : UserControl
    {
        private MongoDatabase mongoDatabase;
        public ElementIncidentGraph()
        {
            InitializeComponent();

            mongoDatabase = new MongoDatabase();

            InitGraph();
        }

        public void InitGraph() {
            List<BsonDocument> element = mongoDatabase.Entretien80element20();

            int total = 0;
            foreach (BsonDocument elem in element)
                total += elem["count"].AsInt32;

            ColumnSeriesCollection = new SeriesCollection{};
            ChartValues<double> values = new();

            foreach (BsonDocument elem in element)
                values.Add(elem["count"].AsInt32 );

            ColumnSeriesCollection.Add(new ColumnSeries
            {
                Title = "",
                Values = values
            });

            LineSeriesCollection = new SeriesCollection{};
            ChartValues<double> valuesLine = new();
            double s = 0;
            foreach (BsonDocument elem in element)
            {
                s += elem["count"].AsInt32;
                valuesLine.Add(s);
            }

            LineSeriesCollection.Add(new LineSeries
            {
                Title = "",
                Values = valuesLine
            });

            Formatter = value => value.ToString("N");
            
            DataContext = this;
        }
        public SeriesCollection ColumnSeriesCollection { get; set; }
        public SeriesCollection LineSeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}
