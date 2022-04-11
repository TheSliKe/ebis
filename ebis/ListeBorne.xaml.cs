using Data;
using Microsoft.Maps.MapControl.WPF;
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
using System.Windows.Shapes;

namespace Ebis
{
    /// <summary>
    /// Logique d'interaction pour ListeBorne.xaml
    /// </summary>
    public partial class ListeBorne : Window
    {

        private MongoDatabase mongoDatabase;

        public ListeBorne()
        {
            InitializeComponent();
            MongoDatabase mongoDatabase = new MongoDatabase();
            
            mongoDatabase.recupererListBorne().ForEach(item => {
                borneList.Items.Add(item);
                double latitude = Convert.ToDouble(item["station"]["latitude"].AsString, System.Globalization.CultureInfo.InvariantCulture);
                double longitude = Convert.ToDouble(item["station"]["longitude"].AsString, System.Globalization.CultureInfo.InvariantCulture);
                Pushpin pin = new();
                pin.Location = new Location(latitude, longitude);
                borneMap.Children.Add(pin);
            });
        }
            mongoDatabase = new MongoDatabase();

            mongoDatabase.recupererListBorne().ForEach(item => borneList.Items.Add(item["station"]["adresseRue"].AsString));
        }

        private void borneList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO Au click: zoom sur la map
            //Location loc = (latitude, longitude);
            //borneMap.SetView(loc, 12);
        }
    }
}
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (!String.IsNullOrEmpty(recherche.Text))
            {
                borneList.Items.Clear();
                mongoDatabase.recupererListBorne(recherche.Text).ForEach(item => borneList.Items.Add(item["station"]["adresseRue"].AsString));
            } else
            {
                borneList.Items.Clear();
                mongoDatabase.recupererListBorne().ForEach(item => borneList.Items.Add(item["station"]["adresseRue"].AsString));
            }
           
        }
    }
}
