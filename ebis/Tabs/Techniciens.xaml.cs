using Data;
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
    /// Logique d'interaction pour Techniciens.xaml
    /// </summary>
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
            List<BsonDocument> listeTechnicien = mongoDatabase.recupererListBorne();

            listeTechnicien.ForEach(item => {

            });
            technicienNom.Text = "TEST";
            technicienMatricule.Text = "TEST";
            technicienAdresse.Text = "TEST";
            technicienVille.Text = "TEST";
            technicienCodePostal.Text = "TEST";
            technicienSecteur.Text = "TEST";
            technicienTelephone.Text = "TEST";

        }
        private void technicienRecherche_TextChanged(object sender, TextChangedEventArgs e) { }
        private void technicienList_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

    }
}
