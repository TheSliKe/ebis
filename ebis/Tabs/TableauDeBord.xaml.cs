using Data;
using LiveCharts;
using LiveCharts.Wpf;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ebis.Tabs
{
    /// <summary>
    /// Logique d'interaction pour TableauDeBord.xaml
    /// </summary>
    public partial class TableauDeBord : UserControl
    {
        private MongoDatabase mongoDatabase;

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public TableauDeBord()
        {
            InitializeComponent();

            mongoDatabase = new MongoDatabase();

            initGraphMoyenneAccident();
            initListElementDefectueux();
            initListElementFiable();

        }

        private void initListElementDefectueux()
        {
            List<BsonDocument> listElementDefectueux = mongoDatabase.statElementDefectueux();
            listElementDefectueux.ForEach(item =>
            {
                ListBoxItem l = new();
                l.Content = item["_id"].AsString;
                listElementsDefectueux.Items.Add(l);
            });
        }

        private void initListElementFiable()
        {
            List<BsonDocument> listElementFiable = mongoDatabase.statElementFiable();
            listElementFiable.ForEach(item =>
            {
                ListBoxItem l = new();
                l.Content = item["_id"].AsString;
                listElementsFiable.Items.Add(l);
            });
        }

        private void initGraphMoyenneAccident()
        {
            double[] avgValue = mongoDatabase.statMoyenneAccident();

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Nombre moyen d'incidents",
                    Values = new ChartValues<double>
                    {
                        avgValue[0],
                        avgValue[1],
                        avgValue[2],
                        avgValue[3],
                        avgValue[4],
                        avgValue[5],
                        avgValue[6],
                        avgValue[7],
                        avgValue[8],
                        avgValue[9],
                        avgValue[10],
                        avgValue[11]
                    }
                }
            };


            Labels = new[] { "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Aout", "Septembre", "Octobre", "Novembre", "Décembre" };
            Formatter = value => value.ToString("N");

            DataContext = this;
        }
      
    }
}
