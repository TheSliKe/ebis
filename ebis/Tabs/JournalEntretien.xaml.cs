using Data;
using Microsoft.Maps.MapControl.WPF;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Ebis.Tabs
{
    public partial class JournalEntretien : UserControl
    {
        private readonly MongoDatabase mongoDatabase;

        public JournalEntretien()
        {
            InitializeComponent();
            mongoDatabase = new MongoDatabase();
            InitialiseEntretienList();
        }

        private void InitialiseEntretienList()
        {
            List<BsonDocument> listeEntretien = mongoDatabase.recupererListEntretien();
            listeEntretien.ForEach(item => {
                ListBoxItem listBoxItem = new();
                listBoxItem.Tag = item;
                listBoxItem.Content = item["dateEntretient"].ToUniversalTime() + " - " + item["borne"].AsString;
                journalEntretienList.Items.Add(listBoxItem);
            });
        }

        private void JournalEntretienRecherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(journalEntretienRecherche.Text))
            {
                journalEntretienList.Items.Clear();
                mongoDatabase.recupererListEntretien(journalEntretienRecherche.Text).ForEach(item =>
                {
                    ListBoxItem listBoxItem = new();
                    listBoxItem.Tag = item;
                    listBoxItem.Content = item["dateEntretient"].ToUniversalTime() + " - " + item["borne"].AsString;
                    journalEntretienList.Items.Add(listBoxItem);
                });
            }
            else
            {
                journalEntretienList.Items.Clear();
                mongoDatabase.recupererListEntretien().ForEach(item =>
                {
                    ListBoxItem listBoxItem = new();
                    listBoxItem.Tag = item;
                    listBoxItem.Content = item["dateEntretient"].ToUniversalTime() + " - " + item["borne"].AsString;
                    journalEntretienList.Items.Add(listBoxItem);
                });
            }
        }

        private void JournalEntretienList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            borneMapEntretien.Children.Clear();
            journalEntretienBorne.Content = "";
            listElementVerifier.Items.Clear();
            listElementRemplacer.Items.Clear();

            if (journalEntretienList.SelectedItem != null)
            {
                string tagJson = ((ListBoxItem)journalEntretienList.SelectedItem).Tag.ToString();
                BsonDocument document = BsonDocument.Parse(tagJson);
               
                journalEntretienBorne.Content = document["borne"];
                double latitude = Convert.ToDouble(
                   document["latitude"].AsString,
                   System.Globalization.CultureInfo.InvariantCulture);
                double longitude = Convert.ToDouble(
                    document["longitude"].AsString,
                    System.Globalization.CultureInfo.InvariantCulture);
                Pushpin pin = new();
                pin.Location = new Location(latitude, longitude);
                borneMapEntretien.Children.Add(pin);
                Location loc = new Location(document["latitude"].ToDouble(), document["longitude"].ToDouble());
                borneMapEntretien.SetView(loc, 12);


                matricule.Text = document["technicien"]["matricule"].ToString();
                nom.Text = document["technicien"]["nom"].ToString();
                prenom.Text = document["technicien"]["prenom"].ToString();

                foreach (var item in document["elements"]["elementVerifier"].AsBsonArray)
                {
                    ListBoxItem listBoxItem = new();
                    listBoxItem.Content = item;
                    listElementVerifier.Items.Add(listBoxItem);
                }

                foreach (var item in document["elements"]["elementRemplacer"].AsBsonArray)
                {
                    ListBoxItem listBoxItem = new();
                    listBoxItem.Content = item;
                    listElementRemplacer.Items.Add(listBoxItem);
                }
            }
        }
    }
}
