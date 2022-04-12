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
    /// Logique d'interaction pour Borne.xaml
    /// </summary>
    public partial class Borne : UserControl
    {
        private MongoDatabase mongoDatabase;

        public Borne()
        {
            InitializeComponent();
            mongoDatabase = new MongoDatabase();
            InitialiseBorneTab();
        }

        private void InitialiseBorneTab()
        {
            List<BsonDocument> listeBorne = mongoDatabase.recupererListBorne();

            listeBorne.ForEach(item => {
                ListBoxItem l = new();
                l.Tag = item;
                l.Content = item["station"]["adresseRue"].AsString;
                l.MouseDoubleClick += new MouseButtonEventHandler(new RoutedEventHandler(borneInfoButton_Click));
                borneList.Items.Add(l);

                double latitude = Convert.ToDouble(item["station"]["latitude"].AsString, System.Globalization.CultureInfo.InvariantCulture);
                double longitude = Convert.ToDouble(item["station"]["longitude"].AsString, System.Globalization.CultureInfo.InvariantCulture);
                Pushpin pin = new();
                pin.Location = new Location(latitude, longitude);
                borneMap.Children.Add(pin);
            });
        }

        private void borneRecherche_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (!String.IsNullOrEmpty(borneRecherche.Text))
            {
                borneList.Items.Clear();
                mongoDatabase.recupererListBorne(borneRecherche.Text).ForEach(item =>
                {
                    ListBoxItem l = new();
                    l.Tag = item;
                    l.Content = item["station"]["adresseRue"].AsString;
                    l.MouseDoubleClick += new MouseButtonEventHandler(new RoutedEventHandler(borneInfoButton_Click));
                    borneList.Items.Add(l);
                });
            }
            else
            {
                borneList.Items.Clear();
                mongoDatabase.recupererListBorne().ForEach(item =>
                {
                    ListBoxItem l = new();
                    l.Tag = item;
                    l.Content = item["station"]["adresseRue"].AsString;
                    l.MouseDoubleClick += new MouseButtonEventHandler(new RoutedEventHandler(borneInfoButton_Click));
                    borneList.Items.Add(l);
                });
            }

            borneInfoButton.IsEnabled = false;

        }

        private void borneInfoButton_Click(object sender, RoutedEventArgs e)
        {

            if (borneList.SelectedItem != null)
            {

                string tagJson = ((ListBoxItem)borneList.SelectedItem).Tag.ToString();
                BsonDocument document = BsonDocument.Parse(tagJson);

                InfoBornePopup infoBornePopup = new InfoBornePopup(document);
                infoBornePopup.Show();
            }


        }

        private void borneList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (borneList.SelectedItem != null)
            {
                string tagJson = ((ListBoxItem)borneList.SelectedItem).Tag.ToString();

                BsonDocument document = BsonDocument.Parse(tagJson);

                Location loc = new Location(document["station"]["latitude"].ToDouble(), document["station"]["longitude"].ToDouble());
                borneMap.SetView(loc, 12);

                borneInfoButton.IsEnabled = true;

            }

        }
    }
}
