using Data;
using Ebis.Object;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Ebis.Tabs
{
    public partial class JournalIntervention : UserControl
    {
        private MongoDatabase mongoDatabase;
        public JournalIntervention()
        {
            InitializeComponent();
            mongoDatabase = new MongoDatabase();
            InitialiseTechnicienTab();
        }
        private void InitialiseTechnicienTab()
        {
            List<BsonDocument> listeIntervention= mongoDatabase.recupererListIntervention();

            List<Intervention> interventions = new List<Intervention>();
            listeIntervention.ForEach(item => {
                interventions.Add(new Intervention()
                {
                    numeroInter = item["numeroInter"].ToString(),
                    typeInter = item["typeInter"].ToString(),
                    dateDebut = item["dateDebut"].ToString(),
                    dateFin = item["dateFin"].ToString(),
                    borne = item["borne"].ToString(),
                    secteur = item["Secteur"].ToString(),
                    detailInter = item["detailInter"].ToString(),
                    technicien = item["technicien"]["nom"].ToString() + " " + item["technicien"]["prenom"].ToString()
                });
            });
            InterventionDataGrid.ItemsSource = interventions;

        }
        private void InterventionRecherche_TextChanged(object sender, TextChangedEventArgs e) { }
        
    }

}
