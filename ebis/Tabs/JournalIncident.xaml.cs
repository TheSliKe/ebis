using Data;
using Ebis.Object;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Ebis.Tabs
{
    public partial class JournalIncident : UserControl
    {
        private readonly MongoDatabase mongoDatabase;
        public JournalIncident()
        {
            InitializeComponent();
            mongoDatabase = new MongoDatabase();
            InitialiseIncidentTab();
        }

        private void InitialiseIncidentTab()
        {
            List<Incident> incidents = SetListIncident(mongoDatabase.RecupererListIncidents());
            incidentDataGrid.ItemsSource = incidents;

        }

        private static List<Incident> SetListIncident(List<BsonDocument> list)
        {

            List<Incident> temp = new();
            list.ForEach(item => {
                temp.Add(new Incident()
                {
                    dateIncident = item["dateIncident"].ToUniversalTime(),
                    borne = item["borne"].ToString(),
                    typeIncidents = item["typeIncidents"].ToString(),
                    detailsIncidents = item["detailsIncidents"].ToString(),
                });
            });
            return temp;
        }

        private void JournalIncidentRecherche_TextChanged(object sender, TextChangedEventArgs e) {
            if (!string.IsNullOrEmpty(journalIncidentRecherche.Text))
            {
                incidentDataGrid.ItemsSource = null;

                List<Incident> incidents = SetListIncident(mongoDatabase.RecupererListIncidents(journalIncidentRecherche.Text));
                incidentDataGrid.ItemsSource = incidents;
            }
            else
            {
                incidentDataGrid.ItemsSource = null;
                List<Incident> incidents = SetListIncident(mongoDatabase.RecupererListIncidents());
                incidentDataGrid.ItemsSource = incidents;
            }
        }
    }
}
