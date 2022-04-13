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
            List<Intervention> interventions = new List<Intervention>();
            mongoDatabase.recupererListIntervention().ForEach(item => {
                interventions.Add(new Intervention()
                {
                    numeroInter = item["numeroInter"].ToString(),
                    typeInter = item["typeInter"].ToString(),
                    dateDebut = item["dateDebut"].AsDateTime,
                    dateFin = item["dateFin"].AsDateTime,
                    borne = item["borne"].ToString(),
                    secteur = item["secteur"].ToString(),
                    detailInter = item["detailInter"].ToString(),
                    technicien = item["technicien"]["nom"].ToString() + " " + item["technicien"]["prenom"].ToString()
                });
            });
            InterventionDataGrid.ItemsSource = interventions;

        }
        private void InterventionRecherche_TextChanged(object sender, TextChangedEventArgs e) {
            if (!string.IsNullOrEmpty(InterventionRecherche.Text))
            {
                InterventionDataGrid.ItemsSource = null;
                List<Intervention> interventions = new();
                mongoDatabase.recupererListIntervention(InterventionRecherche.Text).ForEach(item => {
                    interventions.Add(new Intervention()
                    {
                        numeroInter = item["numeroInter"].ToString(),
                        typeInter = item["typeInter"].ToString(),
                        dateDebut = item["dateDebut"].AsDateTime,
                        dateFin = item["dateFin"].AsDateTime,
                        borne = item["borne"].ToString(),
                        secteur = item["secteur"].ToString(),
                        detailInter = item["detailInter"].ToString(),
                        technicien = item["technicien"]["nom"].ToString() + " " + item["technicien"]["prenom"].ToString()
                    });
                });
                InterventionDataGrid.ItemsSource = interventions;
            }
            else
            {
                InterventionDataGrid.ItemsSource = null;
                List<Intervention> interventions = new();
                mongoDatabase.recupererListIntervention().ForEach(item =>
                {
                    interventions.Add(new Intervention()
                    {
                        numeroInter = item["numeroInter"].ToString(),
                        typeInter = item["typeInter"].ToString(),
                        dateDebut = item["dateDebut"].AsDateTime,
                        dateFin = item["dateFin"].AsDateTime,
                        borne = item["borne"].ToString(),
                        secteur = item["secteur"].ToString(),
                        detailInter = item["detailInter"].ToString(),
                        technicien = item["technicien"]["nom"].ToString() + " " + item["technicien"]["prenom"].ToString()
                    });
                });
                InterventionDataGrid.ItemsSource = interventions;
            }
        }
        
    }

}
