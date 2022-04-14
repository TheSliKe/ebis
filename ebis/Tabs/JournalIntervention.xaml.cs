using Data;
using Ebis.Object;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Ebis.Tabs
{
    public partial class JournalIntervention : UserControl
    {
        private readonly MongoDatabase mongoDatabase;
        public JournalIntervention()
        {
            InitializeComponent();
            mongoDatabase = new MongoDatabase();
            InitialiseInterventionTab();
        }
        private void InitialiseInterventionTab()
        {
            List<Intervention> interventions = SetListIntervention(mongoDatabase.RecupererListIntervention());
            InterventionDataGrid.ItemsSource = interventions;
        }

        private static List<Intervention> SetListIntervention(List<BsonDocument> list)
        {
            List<Intervention> temp = new();
            list.ForEach(item => {
                temp.Add(new Intervention()
                {
                    numeroInter = item["numeroInter"].ToString(),
                    typeInter = item["typeInter"].ToString(),
                    dateDebut = item["dateDebut"].ToUniversalTime(),
                    dateFin = item["dateFin"].ToUniversalTime(),
                    borne = item["borne"].ToString(),
                    secteur = item["secteur"].ToString(),
                    detailInter = item["detailInter"].ToString(),
                    technicien = item["technicien"]["nom"].ToString() + " " + item["technicien"]["prenom"].ToString()
                });
            });
            return temp;
        }

        private void InterventionRecherche_TextChanged(object sender, TextChangedEventArgs e) {
            if (!string.IsNullOrEmpty(InterventionRecherche.Text))
            {
                InterventionDataGrid.ItemsSource = null;
                List<Intervention> interventions = SetListIntervention(mongoDatabase.RecupererListIntervention(InterventionRecherche.Text));
                InterventionDataGrid.ItemsSource = interventions;
            }
            else
            {
                InterventionDataGrid.ItemsSource = null;
                List<Intervention> interventions = SetListIntervention(mongoDatabase.RecupererListIntervention());
                InterventionDataGrid.ItemsSource = interventions;
            }
        }
        
    }

}
