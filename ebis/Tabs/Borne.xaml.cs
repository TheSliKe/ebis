using Data;
using Microsoft.Maps.MapControl.WPF;
using MongoDB.Bson;
using System;
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
            InitialiseBorneTab();
        }

        private void InitialiseBorneTab() 
        {
            mongoDatabase.recupererListBorne().ForEach(item => {
                ListBoxItem listBoxItem = new();
                listBoxItem.Tag = item;
                listBoxItem.Content = item["station"]["adresseRue"].AsString;
                listBoxItem.MouseDoubleClick += new MouseButtonEventHandler(new RoutedEventHandler(BorneInfoButton_Click));
                borneList.Items.Add(listBoxItem);

                double latitude = Convert.ToDouble(
                    item["station"]["latitude"].AsString,
                    System.Globalization.CultureInfo.InvariantCulture);
                double longitude = Convert.ToDouble(
                    item["station"]["longitude"].AsString,
                    System.Globalization.CultureInfo.InvariantCulture);
                Pushpin pin = new();
                pin.Location = new(latitude, longitude);
                borneMap.Children.Add(pin);
            });
        }

        private void BorneRecherche_TextChanged(object sender, TextChangedEventArgs e) 
        {
            if (!string.IsNullOrEmpty(borneRecherche.Text))
            {
                borneList.Items.Clear();
                mongoDatabase.recupererListBorne(borneRecherche.Text).ForEach(item => {
                    ListBoxItem listBoxItem = new();
                    listBoxItem.Tag = item;
                    listBoxItem.Content = item["station"]["adresseRue"].AsString;
                    listBoxItem.MouseDoubleClick += new MouseButtonEventHandler(new RoutedEventHandler(BorneInfoButton_Click));
                    borneList.Items.Add(listBoxItem);
                });
            } 
            else
            {
                borneList.Items.Clear();
                mongoDatabase.recupererListBorne().ForEach(item => {
                    ListBoxItem listBoxItem = new();
                    listBoxItem.Tag = item;
                    listBoxItem.Content = item["station"]["adresseRue"].AsString;
                    listBoxItem.MouseDoubleClick += new MouseButtonEventHandler(new RoutedEventHandler(BorneInfoButton_Click));
                    borneList.Items.Add(listBoxItem);
                });
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
                Location loc = new(
                    document["station"]["latitude"].ToDouble(),
                    document["station"]["longitude"].ToDouble());
                borneMap.SetView(loc, 12);
                borneInfoButton.IsEnabled = true;
            }
        }
    }
}
