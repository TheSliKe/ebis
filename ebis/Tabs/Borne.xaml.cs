using Data;
using Microsoft.Maps.MapControl.WPF;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ebis.Tabs
{
    public partial class Borne : UserControl {
        private readonly MongoDatabase mongoDatabase;

        public Borne() 
        {
            InitializeComponent();
            mongoDatabase = new MongoDatabase();
            DefaultListBorneData();
        }

        private void DefaultListBorneData() 
        {
            SetListBorneDataAndSetPin(mongoDatabase.recupererListBorne());
        }

        private void SetListBorneDataAndSetPin(List<BsonDocument> list)
        {
           list.ForEach(item => {
                ListBoxItem listBoxItem = new();
                listBoxItem.Tag = item;
                listBoxItem.Content = item["numero"].AsString;
                listBoxItem.MouseDoubleClick += new MouseButtonEventHandler(new RoutedEventHandler(BorneInfoButton_Click));
                borneList.Items.Add(listBoxItem);

                Pushpin pin = new();
                pin.Location = new(item["station"]["latitude"].ToDouble(), item["station"]["longitude"].ToDouble());
                borneMap.Children.Add(pin);
            });
        }

        private void BorneRecherche_TextChanged(object sender, TextChangedEventArgs e) 
        {

            borneMap.Children.Clear();

            if (!string.IsNullOrEmpty(borneRecherche.Text))
            {
                borneList.Items.Clear();
                SetListBorneDataAndSetPin(mongoDatabase.RecupererListBorne(borneRecherche.Text));
            } 
            else
            {
                borneList.Items.Clear();
                DefaultListBorneData();
            }
            borneInfoButton.IsEnabled = false;
        }

        private void BorneInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (borneList.SelectedItem != null) 
            {
                string tagJson = ((ListBoxItem)borneList.SelectedItem).Tag.ToString();
                BsonDocument document = BsonDocument.Parse(tagJson);
                InfoBornePopup infoBornePopup = new(document);
                infoBornePopup.Show();
            }
        }

        private void BorneList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (borneList.SelectedItem != null) 
            {
                string tagJson = ((ListBoxItem)borneList.SelectedItem).Tag.ToString();
                BsonDocument document = BsonDocument.Parse(tagJson);
                Location loc = new(document["station"]["latitude"].ToDouble(), document["station"]["longitude"].ToDouble());
                borneMap.SetView(loc, 12);
                borneInfoButton.IsEnabled = true;
            }
        }
    }
}
