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
            var filter = Builders<BsonDocument>.Filter.Or(
                Builders<BsonDocument>.Filter.Regex("secteur", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("numero", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("station.adresseRue", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("station.adresseVille", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("station.codePostal", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("typeCharge.libelle", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("typeCharge.puissance", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase)))
            );
            return borneCollection.Find(filter).ToList();

        }
        public List<BsonDocument> recupererListTechniciens()
        {
            var technicienCollection = db.GetCollection<BsonDocument>("techniciens");
            return technicienCollection.Find(new BsonDocument()).ToList();
        }
        public List<BsonDocument> recupererListTechniciens(string sender)
        {
            var technicienCollection = db.GetCollection<BsonDocument>("techniciens");
            var filter = Builders<BsonDocument>.Filter.Or(
                Builders<BsonDocument>.Filter.Regex("matricule", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("nom", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("prenom", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("ville", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("codePostal", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("secteur", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase)))
            );
            return technicienCollection.Find(filter).ToList();
        }
        public List<BsonDocument> recupererListEntretien()
        {
            var entretiensCollection = db.GetCollection<BsonDocument>("entretiens");
            return entretiensCollection.Find(new BsonDocument()).ToList();
        }
        public List<BsonDocument> recupererListEntretien(string sender)
        {
            var entretiensCollection = db.GetCollection<BsonDocument>("entretiens");
            var filter = Builders<BsonDocument>.Filter.Or(
                Builders<BsonDocument>.Filter.Regex("borne.numero", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("technicien.matricule", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("technicien.nom", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("technicien.prenom", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase)))
            );
            return entretiensCollection.Find(filter).ToList();
        }
        public List<BsonDocument> recupererListIntervention()
        {
            var interventionCollection = db.GetCollection<BsonDocument>("interventions");
            return interventionCollection.Find(new BsonDocument()).ToList();
        }
        public List<BsonDocument> recupererListIntervention(string sender)
        {
            var interventionCollection = db.GetCollection<BsonDocument>("interventions");
            var filter = Builders<BsonDocument>.Filter.Or(
                Builders<BsonDocument>.Filter.Regex("numeroInter", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("typeInter", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("detailInter", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("secteur", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("borne", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("technicien.matricule", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("technicien.nom", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("technicien.prenom", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase)))
            );
            return interventionCollection.Find(filter).ToList();
        }
        public List<BsonDocument> recupererListOperation()
        {
            var operationCollection = db.GetCollection<BsonDocument>("operationsRecharge");
            return operationCollection.Find(new BsonDocument()).ToList();
        }
        public List<BsonDocument> recupererListOperation(string sender)
        {
            var operationCollection = db.GetCollection<BsonDocument>("operationsRecharge");
            var filter = Builders<BsonDocument>.Filter.Or(
                Builders<BsonDocument>.Filter.Regex("nbKwHeures", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("borne", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("noContrat", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("usager.nom", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("usager.prenom", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("typeCharge", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase)))
            );
            return operationCollection.Find(filter).ToList();
        }
        public List<BsonDocument> recupererListIncidents()
        {
            var incidentCollection = db.GetCollection<BsonDocument>("incidents");
            return incidentCollection.Find(new BsonDocument()).ToList();
        }
        public List<BsonDocument> recupererListIncidents(string sender)
        {
            var operationCollection = db.GetCollection<BsonDocument>("incidents");
            var filter = Builders<BsonDocument>.Filter.Or(
                Builders<BsonDocument>.Filter.Regex("borne", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("typeIncidents", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("detailsIncidents", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase)))
            );
            return operationCollection.Find(filter).ToList();
        }
    }
}