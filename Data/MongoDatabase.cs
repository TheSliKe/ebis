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
            mongoClient = new MongoClient("mongodb://localhost:27017/");
            db = mongoClient.GetDatabase("ebis");
        }

        public List<BsonDocument> recupererListBorne()
        {
            var borneCollection = db.GetCollection<BsonDocument>("borne");

            return borneCollection.Find(new BsonDocument()).ToList();

        }

        public List<BsonDocument> recupererListBorne(string sender)
        {
            var borneCollection = db.GetCollection<BsonDocument>("borne");

            var regexPattern = @"\b[" + sender + "]\\w+";

            var filter = Builders<BsonDocument>.Filter.Regex("station.adresseVille", new BsonRegularExpression(new Regex(regexPattern)));
           
            return borneCollection.Find(filter).ToList();
        }
    }
}
