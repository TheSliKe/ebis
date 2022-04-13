using Data;
using LiveCharts;
using LiveCharts.Wpf;
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

namespace Ebis.Tabs.Charts
{
    /// <summary>
    /// Logique d'interaction pour MoyenneAccident.xaml
    /// </summary>
    public partial class MoyenneAccident : UserControl
    {

        private MongoDatabase mongoDatabase;

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }


        public MoyenneAccident()
        {
            InitializeComponent();
            mongoDatabase = new MongoDatabase();
            initGraphMoyenneAccident();
        }

        private void initGraphMoyenneAccident()
        {
            double[] avgValue = mongoDatabase.StatMoyenneAccident();

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
