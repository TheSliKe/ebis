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
    /// Logique d'interaction pour TableauDeBord.xaml
    /// </summary>
    public partial class TableauDeBord : UserControl
    {
        private MongoDatabase mongoDatabase;

        public TableauDeBord()
        {
            InitializeComponent();

            mongoDatabase = new MongoDatabase();

            initListElementDefectueux();
            initListElementFiable();

        }

        private void initListElementDefectueux()
        {
            List<BsonDocument> listElementDefectueux = mongoDatabase.StatElementDefectueux();
            listElementDefectueux.ForEach(item =>
            {
                ListBoxItem l = new();
                l.Content = item["_id"].AsString;
                listElementsDefectueux.Items.Add(l);
            });
        }

        private void initListElementFiable()
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
