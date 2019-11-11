using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;

namespace Webapp.App_Start
{
    public class MongoContext
    {
        MongoClient cliente;
     
        public IMongoDatabase bd;

        public MongoContext()
        {

            string connectionString =
              @"mongodb://servidorapp:sTlyoKhJrg0znWt2CP92NLVtIT6OHtWd9YKntpOFsClf8LKFwaStAImTdg2nLJIn9PbxXz2xv9yBShypAXvgzA==@servidorapp.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";
            MongoClientSettings settings = MongoClientSettings.FromUrl(
              new MongoUrl(connectionString)
            );
            settings.SslSettings =
              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);

            bd = mongoClient.GetDatabase("dbTurismo");


        }

    }
}