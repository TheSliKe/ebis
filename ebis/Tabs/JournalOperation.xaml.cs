using Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ebis.Tabs
{
    /// <summary>
    /// Logique d'interaction pour JournalOperation.xaml
    /// </summary>
    public partial class JournalOperation : UserControl
    {
        private MongoDatabase mongoDatabase;

        public JournalOperation()
        {
            InitializeComponent();
            mongoDatabase = new MongoDatabase();
            InitialiseJournalOperationDg();
        }

        private void InitialiseJournalOperationDg() 
        {

            List<BsonDocument> operationDb = mongoDatabase.recupererListOperation();

            List<Operation> operations = new List<Operation>();

            foreach (var line in operationDb)
            {
                operations.Add(new Operation()
                {
                    Borne = line["borne"].ToString(),
                    TypeCharge = line["typeCharge"].ToString(),
                    DateDebut = line["dateHeureDebut"].ToUniversalTime(),
                    DateFin = line["dateHeureFin"].ToUniversalTime(),
                    KwhConsomer = line["nbKwHeures"].ToString()
                });
            }

            dgJournalOperation.ItemsSource = operations;

        }

        private void journalOperationRecherche_TextChanged(object sender, TextChangedEventArgs e) { }
        private void journalOperationList_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
    }
}
