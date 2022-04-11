using System;
using System.Collections.Generic;
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

    }
}
