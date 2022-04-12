using Ebis.Object;
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
using System.Windows.Shapes;

namespace Ebis
{
    /// <summary>
    /// Logique d'interaction pour InfoBornePopup.xaml
    /// </summary>
    public partial class InfoBornePopup : Window
    {
        public InfoBornePopup(BsonDocument document)
        {
            InitializeComponent();
            Title = document["station"]["adresseRue"].ToString();

            miseEnService.Text = document["dateMiseEnService"].ToString();
            derniereRevision.Text = document["dateDerniereRevision"].ToString();

            adresse.Text = document["station"]["adresseRue"].ToString();
            ville.Text = document["station"]["adresseVille"].ToString();
            codePostal.Text = document["station"]["codePostal"].ToString();

            List <TypeCharge> typeCharges = new List<TypeCharge>();

            foreach (var line in document["typeCharge"].AsBsonArray)
            {
                typeCharges.Add(new TypeCharge() { Libelle = line.AsBsonDocument["libelle"].ToString(), Puissance = line.AsBsonDocument["puissance"].ToString() });
            }

            dgTypeCharge.ItemsSource = typeCharges;
        }
    }
}
