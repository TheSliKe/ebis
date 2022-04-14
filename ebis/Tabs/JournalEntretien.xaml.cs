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
            DefaultListEntretienData();
        }

        private void DefaultListEntretienData()
        {
            SetListEntretienData(mongoDatabase.RecupererListEntretien());
        }

        private void SetListEntretienData(List<BsonDocument> list) 
        {

            list.ForEach(item => {
                ListBoxItem listBoxItem = new();
                listBoxItem.Tag = item;
                listBoxItem.Content = item["dateEntretient"].ToUniversalTime().ToString("dd MMM yyyy") + " - " + item["borne"].AsString;
                journalEntretienList.Items.Add(listBoxItem);
            });

        }

        private void JournalEntretienRecherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(journalEntretienRecherche.Text))
            {
                journalEntretienList.Items.Clear();

                SetListEntretienData(mongoDatabase.recupererListEntretien(journalEntretienRecherche.Text));
            }
            else
            {
                journalEntretienList.Items.Clear();
                DefaultListEntretienData();
            }
        }

        private void JournalEntretienList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            journalEntretienBorne.Content = "";
            borneMapEntretien.Children.Clear();
            listElementVerifier.Items.Clear();
            listElementRemplacer.Items.Clear();

            if (journalEntretienList.SelectedItem != null)
            {
                string tagJson = ((ListBoxItem)journalEntretienList.SelectedItem).Tag.ToString();
                BsonDocument document = BsonDocument.Parse(tagJson);

                Location loc = new(document["latitude"].ToDouble(), document["longitude"].ToDouble());
                Pushpin pin = new();
                pin.Location = loc;

                borneMapEntretien.Children.Add(pin);
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
