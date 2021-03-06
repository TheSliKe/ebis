using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        public List<BsonDocument> RecupererListBorne(string sender)
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
        public List<BsonDocument> RecupererListTechniciens()
        {
            var technicienCollection = db.GetCollection<BsonDocument>("techniciens");
            return technicienCollection.Find(new BsonDocument()).ToList();
        }
        public List<BsonDocument> RecupererListTechniciens(string sender)
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
        public List<BsonDocument> RecupererListEntretien()
        {
            var entretiensCollection = db.GetCollection<BsonDocument>("entretiens");
            return entretiensCollection.Find(new BsonDocument()).ToList();
        }
        public List<BsonDocument> recupererListEntretien(string sender)
        {
            var entretiensCollection = db.GetCollection<BsonDocument>("entretiens");
            var filter = Builders<BsonDocument>.Filter.Or(
                Builders<BsonDocument>.Filter.Regex("borne", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("technicien.matricule", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("technicien.nom", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("technicien.prenom", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase)))
            );
            return entretiensCollection.Find(filter).ToList();
        }
        public List<BsonDocument> RecupererListIntervention()
        {
            var interventionCollection = db.GetCollection<BsonDocument>("interventions");
            return interventionCollection.Find(new BsonDocument()).ToList();
        }
        public List<BsonDocument> RecupererListIntervention(string sender)
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
        public List<BsonDocument> RecupererListOperation()
        {
            var operationCollection = db.GetCollection<BsonDocument>("operationsRecharge");
            return operationCollection.Find(new BsonDocument()).ToList();
        }
        public List<BsonDocument> RecupererListOperation(string sender)
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
        public List<BsonDocument> RecupererListIncidents()
        {
            var incidentCollection = db.GetCollection<BsonDocument>("incidents");
            return incidentCollection.Find(new BsonDocument()).ToList();
        }
        public List<BsonDocument> RecupererListIncidents(string sender)
        {
            var operationCollection = db.GetCollection<BsonDocument>("incidents");
            var filter = Builders<BsonDocument>.Filter.Or(
                Builders<BsonDocument>.Filter.Regex("borne", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("typeIncidents", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase))),
                Builders<BsonDocument>.Filter.Regex("detailsIncidents", new BsonRegularExpression(new Regex(sender, RegexOptions.IgnoreCase)))
            );
            return operationCollection.Find(filter).ToList();
        }

        public double[] StatMoyenneAccident()
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

        public List<BsonDocument> StatElementDefectueux()
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

        public List<BsonDocument> StatElementFiable()
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

        public Dictionary<string, double> StatPartNiveauIncident()
        {
            var listeIncidents = db.GetCollection<BsonDocument>("incidents");

            PipelineDefinition<BsonDocument, BsonDocument> pipeline = new BsonDocument[]
            {
                new BsonDocument("$sortByCount", "$niveauIncident")
            };
            List<BsonDocument> liste = listeIncidents.Aggregate(pipeline).ToList();

            Dictionary<string, double> res = new();

            foreach (BsonDocument element in liste)
            {
                res.Add(element["_id"].AsString, element["count"].AsInt32);
            }

            return res;

        }
        public Dictionary<string, double> StatMoyenneDureeFonctionnement()
        {
            var bornes = recupererListBorne();

            List<int> dfAccesReseaux = new List<int>();
            List<int> dfrouteur = new List<int>();
            List<int> dfdisqueSSD = new List<int>();
            List<int> dfdisqueSAS = new List<int>();
            List<int> dfserveur = new List<int>();
            List<int> dfhote = new List<int>();

            bornes.ForEach(borne =>
            {
                var accesReseaux = borne["accesReseaux"].AsBsonArray;
                foreach (var item in accesReseaux)
                {
                    if (!item["DF"].IsBsonNull && IsValide(item["dateRemplacement"].ToUniversalTime())) { dfAccesReseaux.Add(item["DF"].AsInt32); }
                }

                var routeur = borne["routeur"].AsBsonArray;
                foreach (var item in routeur)
                {
                    if (!item["DF"].IsBsonNull && IsValide(item["dateRemplacement"].ToUniversalTime())) { dfrouteur.Add(item["DF"].AsInt32); }
                }

                var disqueSSD = borne["disqueSSD"].AsBsonArray;
                foreach (var item in disqueSSD)
                {
                    if (!item["DF"].IsBsonNull && IsValide(item["dateRemplacement"].ToUniversalTime())) { dfdisqueSSD.Add(item["DF"].AsInt32); }
                }

                var disqueSAS = borne["disqueSAS"].AsBsonArray;
                foreach (var item in disqueSAS)
                {
                    if (!item["DF"].IsBsonNull && IsValide(item["dateRemplacement"].ToUniversalTime())) { dfdisqueSAS.Add(item["DF"].AsInt32); }
                }

                var serveur = borne["serveur"].AsBsonArray;
                foreach (var item in serveur)
                {
                    if (!item["DF"].IsBsonNull && IsValide(item["dateRemplacement"].ToUniversalTime())) { dfserveur.Add(item["DF"].AsInt32); }
                }

                var hote = borne["hote"].AsBsonArray;
                foreach (var item in hote)
                {
                    if (!item["DF"].IsBsonNull && IsValide(item["dateRemplacement"].ToUniversalTime())) { dfhote.Add(item["DF"].AsInt32); }
                }

            });

            var avgMap = new Dictionary<string, double>();

            avgMap["accesReseaux"] = dfAccesReseaux.Average();
            avgMap["routeur"] = dfrouteur.Average();
            avgMap["disqueSSD"] = dfdisqueSSD.Average();
            avgMap["disqueSAS"] = dfdisqueSAS.Average();
            avgMap["serveur"] = dfserveur.Average();
            avgMap["hote"] = dfhote.Average();

            return avgMap;

        }

        private bool IsValide(DateTime dt)
        {
            return dt >= DateTime.Now.AddYears(-5) && dt <= DateTime.Now;
        }

        public Dictionary<int, int> StatTauxIncident(DateTime debut, DateTime fin)
        {

            var collectionIncidents = db.GetCollection<BsonDocument>("incidents");

            PipelineDefinition<BsonDocument, BsonDocument> pipeline = new BsonDocument[]
            {
                 new BsonDocument("$match", new BsonDocument()
                        .Add("dateIncident", new BsonDocument()
                                .Add("$gte", new BsonDateTime(debut))
                                .Add("$lte", new BsonDateTime(fin))
                        )),
                new BsonDocument("$project", new BsonDocument()
                    .Add("nbHRetard", new BsonDocument()
                        .Add("$subtract", new BsonArray()
                            .Add(new BsonDocument()
                                .Add("$divide", new BsonArray()
                                    .Add(new BsonDocument()
                                        .Add("$subtract", new BsonArray()
                                            .Add("$dateResolution")
                                            .Add("$dateIncident")
                                        )
                                    )
                                .Add(3600000.0)
                            )
                        )
                    .Add(6.0)
                    )
                ))
            };

            Dictionary<int, int> nonbreParHeure = new()
            {
                { 0, 0 },
                { 1, 0 },
                { 2, 0 },
                { 4, 0 },
                { 8, 0 },
                { 12, 0 },
                { 16, 0 },
                { 24, 0 }
            };

            collectionIncidents.Aggregate(pipeline).ToList().ForEach(x =>
            {


                if (!x["nbHRetard"].IsBsonNull)
                {
                    double nbHeure = x["nbHRetard"].AsDouble;
                    if (nbHeure > 0 && nbHeure <= 1)
                    {
                        nonbreParHeure[1] += 1;
                    }
                    else if (nbHeure <= 2)
                    {
                        nonbreParHeure[2] += 1;
                    }
                    else if (nbHeure <= 4)
                    {
                        nonbreParHeure[4] += 1;
                    }
                    else if (nbHeure <= 8)
                    {
                        nonbreParHeure[8] += 1;
                    }
                    else if (nbHeure <= 12)
                    {
                        nonbreParHeure[12] += 1;
                    }
                    else if (nbHeure <= 16)
                    {
                        nonbreParHeure[16] += 1;
                    }
                    else if (nbHeure <= 24)
                    {
                        nonbreParHeure[24] += 1;
                    }
                    else if (nbHeure > 24)
                    {
                        nonbreParHeure[0] += 1;
                    }
                }
            });

            return nonbreParHeure;
        }
        public List<BsonDocument> Entretien80element20(){
            var listeEntretien= db.GetCollection<BsonDocument>("entretiens");

            PipelineDefinition<BsonDocument, BsonDocument> pipeline = new BsonDocument[]
            {
                new BsonDocument("$match", new BsonDocument()
                        .Add("dateEntretient", new BsonDocument()
                                .Add("$gte", new BsonDateTime(DateTime.ParseExact("2017-04-01 02:00:00.000+0200", "yyyy-MM-dd HH:mm:ss.fffzzz", CultureInfo.InvariantCulture)))
                        )),
                new BsonDocument("$unwind", new BsonDocument()
                        .Add("path", "$elements.elementRemplacer")
                        .Add("includeArrayIndex", "elementFiable")
                        .Add("preserveNullAndEmptyArrays", new BsonBoolean(false))),
                new BsonDocument("$sortByCount", "$elements.elementRemplacer"),
                new BsonDocument("$sort", new BsonDocument()
                        .Add("count", -1.0))
            };
            List<BsonDocument> liste = listeEntretien.Aggregate(pipeline).ToList();

            
            return liste;
        }

    }
}