using Data;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Windows.Controls;


namespace Ebis.Tabs
{

    public partial class TableauDeBord : UserControl
    {
        private MongoDatabase mongoDatabase;

        public TableauDeBord()
        {
            InitializeComponent();

            mongoDatabase = new MongoDatabase();

            InitListElementDefectueux();
            InitListElementFiable();
        }

        private void InitListElementDefectueux()
        {
            List<BsonDocument> listElementDefectueux = mongoDatabase.StatElementDefectueux();
            listElementDefectueux.ForEach(item =>
            {
                ListBoxItem l = new();
                l.Content = item["_id"].AsString;
                listElementsDefectueux.Items.Add(l);
            });
        }

        private void InitListElementFiable()
        {
            List<BsonDocument> listElementFiable = mongoDatabase.StatElementFiable();
            listElementFiable.ForEach(item =>
            {
                ListBoxItem l = new();
                l.Content = item["_id"].AsString;
                listElementsFiable.Items.Add(l);
            });
        }

      
    }
}
