using Data;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Logique d'interaction pour TauxIncident.xaml
    /// </summary>
    public partial class TauxIncident : UserControl
    {

        public MongoDatabase mongoDatabase;

        public TauxIncident()
        {
            InitializeComponent();
            mongoDatabase = new MongoDatabase();
            InitDatePickers();
            InitTauxIncidentGraph();
        }

        private void InitTauxIncidentGraph()
        {
            SetDataTauxIncidentGraph(mongoDatabase.statTauxIncident(dateDebut.DisplayDate, dateFin.DisplayDate));

        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private void SetDataTauxIncidentGraph(Dictionary<int, int> tauxIncidentParHoraire) 
        {

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Taux d'incidents en retard : ",
                    Values = new ChartValues<double>
                    {
                        tauxIncidentParHoraire[1],
                        tauxIncidentParHoraire[2],
                        tauxIncidentParHoraire[4],
                        tauxIncidentParHoraire[8],
                        tauxIncidentParHoraire[12],
                        tauxIncidentParHoraire[16],
                        tauxIncidentParHoraire[24],
                        tauxIncidentParHoraire[0]
                    }
                }
            };

           

            Labels = new[] { "0-1h", "1-2h", "2-4h", "4-8h", "8-12h", "12-16h", "16-24h", "+24h" };
            Formatter = value => value.ToString("N");

            DataContext = this;

        }

        private void InitDatePickers()
        {
            dateDebut.SelectedDate = DateTime.ParseExact("01/01/1970", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dateFin.SelectedDate = DateTime.Now;
        }

        private void DateChangement(object sender, RoutedEventArgs e)
        {
            SeriesCollection.Clear();
            Dictionary<int, int> tauxIncidentParHoraire = mongoDatabase.statTauxIncident(dateDebut.DisplayDate, dateFin.DisplayDate);
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Taux d'incidents en retard : ",
                Values = new ChartValues<double>
                    {
                        tauxIncidentParHoraire[1],
                        tauxIncidentParHoraire[2],
                        tauxIncidentParHoraire[4],
                        tauxIncidentParHoraire[8],
                        tauxIncidentParHoraire[12],
                        tauxIncidentParHoraire[16],
                        tauxIncidentParHoraire[24],
                        tauxIncidentParHoraire[0]
                    }
            });
        }
    }
}
