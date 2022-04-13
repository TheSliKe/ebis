using Data;
using Ebis.Object;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Ebis.Tabs
{
    public partial class Techniciens : UserControl
    {
        private readonly MongoDatabase mongoDatabase;
        public Techniciens()
        {
            InitializeComponent();
            mongoDatabase = new MongoDatabase();
            InitialiseTechnicienTab();
        }
        private void InitialiseTechnicienTab()
        {
            List<BsonDocument> listeTechnicien = mongoDatabase.recupererListTechniciens();

            listeTechnicien.ForEach(item => {
                ListBoxItem listBoxItem = new();
                listBoxItem.Tag = item;
                listBoxItem.Content = item["nom"].AsString + " " + item["prenom"].AsString;
                technicienList.Items.Add(listBoxItem);
            });
        }
        private void TechnicienRecherche_TextChanged(object sender, TextChangedEventArgs e) {
            if (!string.IsNullOrEmpty(technicienRecherche.Text)) {
                technicienList.Items.Clear();
                mongoDatabase.recupererListTechniciens(technicienRecherche.Text).ForEach( item =>
                {
                    ListBoxItem listBoxItem = new();
                    listBoxItem.Tag = item;
                    listBoxItem.Content = item["nom"].AsString + " " + item["prenom"].AsString;
                    technicienList.Items.Add(listBoxItem);
                });
            } 
            else
            {
                technicienList.Items.Clear();
                mongoDatabase.recupererListTechniciens().ForEach(item =>
               {
                   ListBoxItem listBoxItem = new();
                   listBoxItem.Tag = item;
                   listBoxItem.Content = item["nom"].AsString + " " + item["prenom"].AsString;
                   technicienList.Items.Add(listBoxItem);
               });
            }
        }
        private void TechnicienList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (technicienList.SelectedItem != null)
            {
                string tagJson = ((ListBoxItem)technicienList.SelectedItem).Tag.ToString();
                BsonDocument document = BsonDocument.Parse(tagJson);
                technicienNom.Text = document["nom"].ToString();
                technicienPrenom.Text = document["prenom"].ToString();
                technicienMatricule.Text = document["matricule"].ToString();
                technicienAdresse.Text = document["adresse"].ToString();
                technicienVille.Text = document["ville"].ToString();
                technicienCodePostal.Text = document["codePostal"].ToString();
                technicienSecteur.Text = document["secteur"].ToString();
                technicienTelephone.Text = document["telMobile"].ToString();

                List<Intervention> interventions = new List<Intervention>();

                foreach (var line in document["intervention"].AsBsonArray)
                {
                    interventions.Add(new Intervention()
                    {
                        numeroInter = line["numeroInter"].ToString(),
                        typeInter = line["typeInter"].ToString(),
                        dateDebut = line["dateDebut"].AsDateTime,
                        dateFin = line["dateFin"].AsDateTime,
                    });
                }
                technicienInterventionList.ItemsSource = interventions;
            }
        }
    }
}
