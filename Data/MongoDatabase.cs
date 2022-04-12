using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Data
{
    public class MongoDatabase
    {

        private MongoClient mongoClient;
        private IMongoDatabase db;

        public MongoDatabase()
        {
            mongoClient = new MongoClient("mongodb+srv://ebis:ebis@ebis.03sbd.mongodb.net/");
            db = mongoClient.GetDatabase("Ebis");
        }

        public List<BsonDocument> recupererListBorne()
        {
            var borneCollection = db.GetCollection<BsonDocument>("bornes");

            return borneCollection.Find(new BsonDocument()).ToList();

        }

        public List<BsonDocument> recupererListBorne(string sender)
        {
            var borneCollection = db.GetCollection<BsonDocument>("bornes");

            var regexPattern = @"\b[" + sender + "]\\w+";

            var filter = Builders<BsonDocument>.Filter.Regex("station.adresseVille", new BsonRegularExpression(new Regex(regexPattern)));
           
            return borneCollection.Find(filter).ToList();
        }

        public List<BsonDocument> recupererListTechniciens()
        {
            var borneCollection = db.GetCollection<BsonDocument>("techniciens");
            return borneCollection.Find(new BsonDocument()).ToList();
        }

        public List<BsonDocument> recupererListEntretien()
        {
            var entretiensCollection = db.GetCollection<BsonDocument>("entretiens");

            return entretiensCollection.Find(new BsonDocument()).ToList();
        }
        public List<BsonDocument> recupererListIntervention()
        {
            var borneCollection = db.GetCollection<BsonDocument>("interventions");
            return borneCollection.Find(new BsonDocument()).ToList();
        }

    }
}
