using Data;
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
            mongoDatabase = new MongoDatabase();

            mongoDatabase.recupererListBorne().ForEach(item => borneList.Items.Add(item["station"]["adresseRue"].AsString));
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
