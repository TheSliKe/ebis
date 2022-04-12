using Data;
using Microsoft.Maps.MapControl.WPF;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
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

namespace Ebis.Tabs
{
    /// <summary>
    /// Logique d'interaction pour JournalEntretien.xaml
    /// </summary>
    public partial class JournalEntretien : UserControl
    {
        private MongoDatabase mongoDatabase;

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
                ListBoxItem l = new();
                l.Tag = item;
                l.Content = item["dateEntretient"].ToUniversalTime();
                journalEntretienList.Items.Add(l);
                
            });
        }

        private void journalEntretienRecherche_TextChanged(object sender, TextChangedEventArgs e) { }

        
        private void journalEntretienList_SelectionChangedBorneConcerne(object sender, SelectionChangedEventArgs e)
        {
            
            if(listeBorneConcerne.SelectedItem != null)
            {

                string tagJson = ((ListBoxItem)listeBorneConcerne.SelectedItem).Tag.ToString();
                BsonDocument document = BsonDocument.Parse(tagJson);


                borneMapEntretien.Children.Clear();

                double latitude = Convert.ToDouble(document["latitude"].AsString, System.Globalization.CultureInfo.InvariantCulture);
                double longitude = Convert.ToDouble(document["longitude"].AsString, System.Globalization.CultureInfo.InvariantCulture);
                Pushpin pin = new();
                pin.Location = new Location(latitude, longitude);
                borneMapEntretien.Children.Add(pin);
                Location loc = new Location(document["latitude"].ToDouble(), document["longitude"].ToDouble());
                borneMapEntretien.SetView(loc, 12);

            }

        }
            private void journalEntretienList_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {

            listeBorneConcerne.Items.Clear();
            listElementVerifier.Items.Clear();
            listElementRemplacer.Items.Clear();

            if (journalEntretienList.SelectedItem != null)
            {

                string tagJson = ((ListBoxItem)journalEntretienList.SelectedItem).Tag.ToString();
                BsonDocument document = BsonDocument.Parse(tagJson);

                foreach (var item in document["borne"].AsBsonArray)
                {
                    ListBoxItem l = new();
                    l.Tag = item;
                    l.Content = item["numero"].AsString;
                    listeBorneConcerne.Items.Add(l);
                }

                matricule.Text = document["technicien"]["matricule"].ToString();
                nom.Text = document["technicien"]["nom"].ToString();
                prenom.Text = document["technicien"]["prenom"].ToString();

                foreach (var item in document["elements"]["elementVerifier"].AsBsonArray)
                {
                    ListBoxItem l = new();
                    l.Content = item;
                    listElementVerifier.Items.Add(l);
                }

                foreach (var item in document["elements"]["elementRemplacer"].AsBsonArray)
                {
                    ListBoxItem l = new();
                    l.Content = item;
                    listElementRemplacer.Items.Add(l);
                }

            }

        }
    }
}
