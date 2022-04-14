using Ebis.Object;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Windows;


namespace Ebis
{
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
