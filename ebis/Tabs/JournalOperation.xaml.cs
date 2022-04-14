using Data;
using Ebis.Object;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Ebis.Tabs
{
    public partial class JournalOperation : UserControl
    {
        private readonly MongoDatabase mongoDatabase;
        public JournalOperation()
        {
            InitializeComponent();
            mongoDatabase = new MongoDatabase();
            InitialiseJournalOperationTab();
        }

        private void InitialiseJournalOperationTab() 
        {

            List<Operation> operations = SetListOperation(mongoDatabase.RecupererListOperation());
            dgJournalOperation.ItemsSource = operations;

        }

        private static List<Operation> SetListOperation(List<BsonDocument> list)
        {

            List<Operation> temp = new();
            list.ForEach(item => {
                temp.Add(new Operation()
                {
                    Borne = item["borne"].ToString(),
                    TypeCharge = item["typeCharge"].ToString(),
                    DateDebut = item["dateHeureDebut"].ToUniversalTime(),
                    DateFin = item["dateHeureFin"].ToUniversalTime(),
                    KwhConsomer = item["nbKwHeures"].ToString()
                });
            });
            return temp;

        }

            private void JournalOperationRecherche_TextChanged(object sender, TextChangedEventArgs e) {
            
            if (!string.IsNullOrEmpty(journalOperationRecherche.Text))
            {
                dgJournalOperation.ItemsSource = null;
                List<Operation> operations = SetListOperation(mongoDatabase.RecupererListOperation(journalOperationRecherche.Text));
                dgJournalOperation.ItemsSource = operations;
            }
            else
            {
                dgJournalOperation.ItemsSource = null;
                List<Operation> operations = SetListOperation(mongoDatabase.RecupererListOperation());
                dgJournalOperation.ItemsSource = operations;
            }
        }
    }
}
