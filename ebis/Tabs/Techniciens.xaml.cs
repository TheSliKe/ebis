using Data;
using Ebis.Object;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Ebis.Tabs
{
    public partial class Techniciens : UserControl
    {

        private MongoDatabase mongoDatabase;

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
                ListBoxItem l = new();
                l.Tag = item;
                l.Content = item["nom"].AsString + " " + item["prenom"].AsString;
                technicienList.Items.Add(l);
            });


        }
        private void technicienRecherche_TextChanged(object sender, TextChangedEventArgs e) { }
        private void technicienList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string tagJson = ((ListBoxItem)technicienList.SelectedItem).Tag.ToString();

            BsonDocument document = BsonDocument.Parse(tagJson);

            technicienNom.Text = document["nom"].ToString();
            technicienPrenom.Text = document["prenom"].ToString();
            technicienMatricule.Text = document["matricule"].ToString();
            technicienAdresse.Text = document["adresse"].ToString();
            technicienVille.Text = document["ville"].ToString();
            technicienCodePostal.Text = document["codePostal"].ToString();
            technicienSecteur.Text = document["Secteur"].ToString();
            technicienTelephone.Text = document["telMobile"].ToString();

            List<Intervention> interventions = new List<Intervention>();

            foreach (var line in document["intervention"].AsBsonArray)
            {
                interventions.Add(new Intervention()
                {
                    numeroInter = line["numeroInter"].ToString(),
                    typeInter = line["typeInter"].ToString(),
                    dateDebut = line["dateDebut"].ToString(),
                    dateFin = line["dateFin"].ToString(),
                });
            }

            technicienInterventionList.ItemsSource = interventions;
        }

    }
}
