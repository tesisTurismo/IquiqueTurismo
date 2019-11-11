using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace appTurismoIqq.Modelo
{
    public class Administrador
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [BsonElement("nombreAdmin")]
        public string nombreAdmin { get; set; }
  
        [BsonElement("password")]
        public string password { get; set; }
    }
}
