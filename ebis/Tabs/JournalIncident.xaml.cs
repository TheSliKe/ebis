﻿using Data;
using Ebis.Object;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Ebis.Tabs
{
    public partial class JournalIncident : UserControl
    {
        private MongoDatabase mongoDatabase;
        public JournalIncident()
        {
            InitializeComponent();
            mongoDatabase = new MongoDatabase();
            InitialiseIncidentTab();
        }

        private void InitialiseIncidentTab()
        {
            List<Incident> incidents = new();
            mongoDatabase.recupererListIncidents().ForEach(item => {
                incidents.Add(new Incident()
                {
                    dateIncident = item["dateIncident"].AsDateTime,
                    borne = item["borne"].ToString(),
                    typeIncidents = item["typeIncidents"].ToString(),
                    detailsIncidents = item["detailsIncidents"].ToString(),
                });
            });
            interventionDataGrid.ItemsSource = incidents;

        }

        private void journalIncidentRecherche_TextChanged(object sender, TextChangedEventArgs e) {
            if (!string.IsNullOrEmpty(journalIncidentRecherche.Text))
            {
                interventionDataGrid.ItemsSource = null;
                List<Incident> incidents = new();
                mongoDatabase.recupererListIncidents(journalIncidentRecherche.Text).ForEach(item => {
                    incidents.Add(new Incident()
                    {
                        dateIncident = item["dateIncident"].AsDateTime,
                        borne = item["borne"].ToString(),
                        typeIncidents = item["typeIncidents"].ToString(),
                        detailsIncidents = item["detailsIncidents"].ToString(),
                    });
                }); 
                interventionDataGrid.ItemsSource = incidents;
            }
            else
            {
                interventionDataGrid.ItemsSource = null;
                List<Incident> incidents = new();
                mongoDatabase.recupererListIncidents().ForEach(item =>
                {
                    incidents.Add(new Incident()
                    {
                        dateIncident = item["dateIncident"].AsDateTime,
                        borne = item["borne"].ToString(),
                        typeIncidents = item["typeIncidents"].ToString(),
                        detailsIncidents = item["detailsIncidents"].ToString(),
                    });
                }); 
                interventionDataGrid.ItemsSource = incidents;
            }
        }
    }
}
