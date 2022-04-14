using Data;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows.Controls;


namespace Ebis.Tabs.Charts
{
    public partial class MoyenneFonctionnement : UserControl
    {

        private MongoDatabase mongoDatabase;

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public MoyenneFonctionnement()
        {
            InitializeComponent();
            mongoDatabase = new MongoDatabase();
            InitGraphMoyenneFonctionnement();
        }


        private void InitGraphMoyenneFonctionnement()
        {
            Dictionary<string, double> avgMap = mongoDatabase.StatMoyenneDureeFonctionnement();

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Durée moyenne de fonctionnement en Jour :",
                    Values = new ChartValues<double>
                    {
                       avgMap["accesReseaux"],
                       avgMap["routeur"],
                       avgMap["disqueSSD"],
                       avgMap["disqueSAS"],
                       avgMap["serveur"],
                       avgMap["hote"]
                    }
                }
            };

            Labels = new[] { "accesReseaux", "routeur", "disqueSSD", "disqueSAS", "serveur", "hote"};
            Formatter = value => value.ToString("N");

            DataContext = this;
        }
    }
}
