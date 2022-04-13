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

        private void JournalOperationRecherche_TextChanged(object sender, TextChangedEventArgs e) {
            
            if (!string.IsNullOrEmpty(journalOperationRecherche.Text))
            {
                dgJournalOperation.ItemsSource = null;
                List<Operation> operations = new();
                mongoDatabase.recupererListOperation(journalOperationRecherche.Text).ForEach(item => {
                    operations.Add(new Operation()
                    {
                        Borne = item["borne"].ToString(),
                        TypeCharge = item["typeCharge"].ToString(),
                        DateDebut = item["dateHeureDebut"].ToUniversalTime(),
                        DateFin = item["dateHeureFin"].ToUniversalTime(),
                        KwhConsomer = item["nbKwHeures"].ToString()
                    });
                });
                dgJournalOperation.ItemsSource = operations;
            }
            else
            {
                dgJournalOperation.ItemsSource = null;
                List<Operation> operations = new();
                mongoDatabase.recupererListOperation().ForEach(item =>
                {
                    operations.Add(new Operation()
                    {
                        Borne = item["borne"].ToString(),
                        TypeCharge = item["typeCharge"].ToString(),
                        DateDebut = item["dateHeureDebut"].ToUniversalTime(),
                        DateFin = item["dateHeureFin"].ToUniversalTime(),
                        KwhConsomer = item["nbKwHeures"].ToString()
                    });
                });
                dgJournalOperation.ItemsSource = operations;
            }
        }
    }
}
