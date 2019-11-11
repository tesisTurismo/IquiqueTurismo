using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace appTurismoIqq.Modelo
{
    public class Usuario
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [BsonElement("nombreU")]
        public string nombreU { get; set; }
        [BsonElement("apellidoU")]
        public string apellidoU { get; set; }
        [BsonElement("passwordU")]
        public string passwordU { get; set; }
        [BsonElement("paisOrigen")]
        public string paisOrigen { get; set; }
        [BsonElement("ciudadOrigen")]
        public string ciudadOrigen { get; set; }
        [BsonElement("nacionalidad")]
        public string nacionalidad { get; set; }
        [BsonElement("email")]
        public string email { get; set; }
       // [BsonElement("motivoVisita")]
       // public string motivoVisita { get; set; }

    }
}
