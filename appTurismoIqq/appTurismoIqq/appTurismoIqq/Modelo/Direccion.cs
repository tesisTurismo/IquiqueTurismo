using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace appTurismoIqq.Modelo
{
    public class Direccion
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("direccion")]
        public string direccion { get; set; }

        [BsonElement("latitud")]
        public double latitud { get; set; }
        [BsonElement("longitud")]
        public double longitud { get; set; }

        [BsonElement("entidad")]
        public string entidad { get; set; }

        [BsonElement("telefono")]
        public string telefono { get; set; }

        




    }
}
