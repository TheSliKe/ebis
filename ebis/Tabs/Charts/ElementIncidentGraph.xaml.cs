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

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private void InitGraph() {

            List<BsonDocument> listElements = mongoDatabase.Entretien80element20();

            Dictionary<string, int> temp = new Dictionary<string, int>();
            foreach (BsonDocument element in listElements) {

                temp[element["_id"].AsString] = element["count"].AsInt32;

            }

            int[] cumul = new int[listElements.Count];
            int Index = 0;
            listElements.ForEach(x =>
            {
                if (Index == 0) 
                {
                    cumul[Index] = x["count"].AsInt32;
                } 
                else 
                {
                    cumul[Index] = x["count"].AsInt32 + cumul[Index - 1];
                }

                Index++;
            });

            double[] pourcentage = new double[listElements.Count];
            Index = 0;
            listElements.ForEach(x =>
            {
                pourcentage[Index] = (double)cumul[Index] / (double)cumul[5] * (double)100;

                Index++;
            });


            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "element",
                    Values = new ChartValues<double>
                    {
                        temp["routeur"],
                        temp["serveur"],
                        temp["disqueSas"],
                        temp["accesReseaux"],
                        temp["disqueSsd"],
                        temp["hote"]
                    }
                },
                new LineSeries
                {
                    Title = "pourcentage",
                    Values = new ChartValues<double>
                    {
                        pourcentage[0],
                        pourcentage[1],
                        pourcentage[2],
                        pourcentage[3],
                        pourcentage[4],
                        pourcentage[5]
                    }

                }
            };

            Labels = new string[listElements.Count];

            Index = 0;
            listElements.ForEach(x =>
            {
                Labels[Index] = x["_id"].AsString;
                Index++;
            });

            Formatter = value => value.ToString("N");

            DataContext = this;
        }

       

    }
}
