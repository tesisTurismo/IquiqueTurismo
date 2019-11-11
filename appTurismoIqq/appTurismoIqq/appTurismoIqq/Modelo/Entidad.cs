using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace appTurismoIqq.Modelo
{
    public class Entidad
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [BsonElement ("foto")]
        public string foto { get; set; }
        [BsonElement("nombre")]
        public string nombre { get; set; }
        [BsonElement("pagWeb")]
        public string pagWeb { get; set; }
        [BsonElement("descripcion")]
        public string descripcion { get; set; }
        [BsonElement("descripcionEng")]
        public string descripcionEng { get; set; }

      

        [BsonElement("categoria")]
        public string categoria { get; set; }

        [BsonElement("vistas")]
        public int vistas { get; set; }



        public string fotoApp
        {
            get
            {
                if (string.IsNullOrEmpty(this.foto))
                {
                    return "casa";
                }

                return $"https://appturismoweb20191022092238.azurewebsites.net/{this.foto.Substring(1)}";

            }
        }
    }
}
