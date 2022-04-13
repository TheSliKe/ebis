using System;
using System.Collections.Generic;
using System.Globalization;
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

        public List<BsonDocument> recupererListEntretien(string sender)
        {
            var borneCollection = db.GetCollection<BsonDocument>("entretiens");

            var regexPattern = @"\b[" + sender + "]\\w+";

            var filter = Builders<BsonDocument>.Filter.Regex("technicien.matricule", new BsonRegularExpression(new Regex(regexPattern)));

            return borneCollection.Find(filter).ToList();
        }

        public List<BsonDocument> recupererListIntervention()
        {
            var borneCollection = db.GetCollection<BsonDocument>("interventions");
            return borneCollection.Find(new BsonDocument()).ToList();
        }
        public List<BsonDocument> recupererListOperation()
        {
            var operationCollection = db.GetCollection<BsonDocument>("operationsRecharge");
            return operationCollection.Find(new BsonDocument()).ToList();
        }
        public List<BsonDocument> recupererListIncidents()
        {
            var operationCollection = db.GetCollection<BsonDocument>("incidents");
            return operationCollection.Find(new BsonDocument()).ToList();
        }

        public double[] statMoyenneAccident()
        {
            var incidentsCollection = db.GetCollection<BsonDocument>("incidents");

            double[] avgValue = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            PipelineDefinition<BsonDocument, BsonDocument> pipeline5Years = new BsonDocument[]
            {
            new BsonDocument("$addFields", new BsonDocument()
                    .Add("month", new BsonDocument()
                            .Add("$month", "$dateIncident")
                    )
                ),
            new BsonDocument("$match", new BsonDocument()
                    .Add("dateIncident", new BsonDocument()
                            .Add("$gte", new BsonDateTime(DateTime.Now.AddYears(-5)))
                    )
                ),
            new BsonDocument("$group", new BsonDocument()
                    .Add("_id", new BsonDocument()
                            .Add("$month", "$dateIncident")
                    )
                    .Add("avg", new BsonDocument()
                            .Add("$sum", 0.2)
                    )   
            )};

            PipelineDefinition<BsonDocument, BsonDocument> pipeline6Years = new BsonDocument[]
            {
            new BsonDocument("$addFields", new BsonDocument()
                    .Add("month", new BsonDocument()
                            .Add("$month", "$dateIncident")
                    )
                ),
            new BsonDocument("$match", new BsonDocument()
                    .Add("dateIncident", new BsonDocument()
                            .Add("$gte", new BsonDateTime(DateTime.Now.AddYears(-6)))
                    )
                ),
            new BsonDocument("$group", new BsonDocument()
                    .Add("_id", new BsonDocument()
                            .Add("$month", "$dateIncident")
                    )
                    .Add("avg", new BsonDocument()
                            .Add("$sum", 0.2)
                    )
            )};

            List<BsonDocument> stats5Years = incidentsCollection.Aggregate(pipeline5Years).ToList();
            List<BsonDocument> stats6Years = incidentsCollection.Aggregate(pipeline6Years).ToList();

            stats5Years.ForEach(item =>
            {
                avgValue[item["_id"].AsInt32 - 1] = item["avg"].AsDouble;
            });

            stats6Years.ForEach(item =>
            {

                var date = DateTime.Now;
                if (item["_id"].AsInt32 > date.Month)
                {
                    avgValue[item["_id"].AsInt32 - 1] = item["avg"].AsDouble;
                }

            });

            return avgValue;
        }

        public List<BsonDocument> statElementDefectueux()
        {

            var incidentsEntretien = db.GetCollection<BsonDocument>("entretiens");

            PipelineDefinition<BsonDocument, BsonDocument> pipeline = new BsonDocument[]
            {
                new BsonDocument("$match", new BsonDocument()
                        .Add("dateEntretient", new BsonDocument()
                                .Add("$gte", new BsonDateTime(DateTime.Now.AddYears(-5)))
                        )),
                new BsonDocument("$unwind", new BsonDocument()
                        .Add("path", "$elements.elementRemplacer")
                        .Add("includeArrayIndex", "elementDefectueux")
                        .Add("preserveNullAndEmptyArrays", new BsonBoolean(false))),
                new BsonDocument("$sortByCount", "$elements.elementRemplacer"),
                new BsonDocument("$sort", new BsonDocument()
                        .Add("count", -1.0)),
                new BsonDocument("$limit", 5.0)
            };

            return incidentsEntretien.Aggregate(pipeline).ToList();

        }

        public List<BsonDocument> statElementFiable()
        {

            var incidentsEntretien = db.GetCollection<BsonDocument>("entretiens");

            PipelineDefinition<BsonDocument, BsonDocument> pipeline = new BsonDocument[]
            {
                new BsonDocument("$match", new BsonDocument()
                        .Add("dateEntretient", new BsonDocument()
                                .Add("$gte", new BsonDateTime(DateTime.Now.AddYears(-5)))
                        )),
                new BsonDocument("$unwind", new BsonDocument()
                        .Add("path", "$elements.elementRemplacer")
                        .Add("includeArrayIndex", "elementFiable")
                        .Add("preserveNullAndEmptyArrays", new BsonBoolean(false))),
                new BsonDocument("$sortByCount", "$elements.elementRemplacer"),
                new BsonDocument("$sort", new BsonDocument()
                        .Add("count", 1.0)),
                new BsonDocument("$limit", 5.0)
            };

            return incidentsEntretien.Aggregate(pipeline).ToList();
        }

    }
}
