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
    /// Logique d'interaction pour MoyenneFonctionnement.xaml
    /// </summary>
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
            initGraphMoyenneFonctionnement();
        }


        private void initGraphMoyenneFonctionnement()
        {
            Dictionary<string, double> avgMap = mongoDatabase.statMoyenneDureeFonctionnement();

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
