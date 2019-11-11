using appTurismoIqq.Modelo;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;

namespace appTurismoIqq.Servicios
{
    
    public class ApiServicio
    {
        MongoClient cliente;
        IMongoDatabase database;
        public string conec = "mongodb+srv://user:tesis123456@tesis1-7onzc.azure.mongodb.net/test";
        public string bdname = "bdTurismo";
        string collectionName = "entidad";

        public string error { get; set; }
        public string coleccionSeleccionada { get; set; }

        public async Task<Respuesta> ValidacionInternet()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Respuesta
                {
                    respExitosa = false,
                    mensaje = "Por favor activar su conexión WiFi",
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("www.google.com");
            if (!isReachable)
            {
                return new Respuesta
                {
                    respExitosa = false,
                    mensaje = "No hay conexión a internet.",
                };
            }

            return new Respuesta
            {
                respExitosa = true,
            };
        }

        IMongoCollection<Entidad> coleccionEntidades;
         IMongoCollection<Entidad> ColeccionEntidades
        
        {
            get
            {
                if (coleccionEntidades == null)
                {
                    string connectionString =

  @"mongodb://appturismo:0hSQ4nkxAj325uSDCe4QRmCj9czKA4jHymyvt5XIZrd4g4Tr38vk549MnftCB1nHA8EE1G4PxqeAVBjL8BWq5A==@appturismo.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@appturismo@";

                    MongoClientSettings settings = MongoClientSettings.FromUrl(
                      new MongoUrl(connectionString)
                    );
                    settings.SslSettings =
                      new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                    var mongoClient = new MongoClient(settings);
                    //var client = new MongoClient(conec);
                    var db = mongoClient.GetDatabase(bdname);
                    var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
                    coleccionEntidades = db.GetCollection<Entidad>("Entidad", collectionSettings);
                }

                return coleccionEntidades;
            }
        
            
        }


        IMongoCollection<Categoria> coleccionCategoria;
        IMongoCollection<Categoria> ColeccionCategoria

        {
            get
            {
                if (coleccionCategoria == null)
                {
                    string connectionString =

  @"mongodb://appturismo:0hSQ4nkxAj325uSDCe4QRmCj9czKA4jHymyvt5XIZrd4g4Tr38vk549MnftCB1nHA8EE1G4PxqeAVBjL8BWq5A==@appturismo.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@appturismo@";

    

                    MongoClientSettings settings = MongoClientSettings.FromUrl(
                      new MongoUrl(connectionString)
                    );
                    settings.SslSettings =
                      new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                    var mongoClient = new MongoClient(settings);
                    //var client = new MongoClient(conec);
                    var db = mongoClient.GetDatabase(bdname);
                    var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
                    coleccionCategoria = db.GetCollection<Categoria>("Categoria", collectionSettings);
                    


                  

                }

                return coleccionCategoria;
            }

        }

        IMongoCollection<Direccion> coleccionDireccion;
        IMongoCollection<Direccion> ColeccionDireccion

        {
            get
            {
                if (coleccionDireccion == null)
                {
                    string connectionString =

  @"mongodb://appturismo:0hSQ4nkxAj325uSDCe4QRmCj9czKA4jHymyvt5XIZrd4g4Tr38vk549MnftCB1nHA8EE1G4PxqeAVBjL8BWq5A==@appturismo.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@appturismo@";

                    MongoClientSettings settings = MongoClientSettings.FromUrl(
                      new MongoUrl(connectionString)
                    );
                    settings.SslSettings =
                      new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                    var mongoClient = new MongoClient(settings);
                    //var client = new MongoClient(conec);
                    var db = mongoClient.GetDatabase(bdname);
                    var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };

                    coleccionDireccion = db.GetCollection<Direccion>("Direccion", collectionSettings);
                }

                return coleccionDireccion;
            }

        }


        IMongoCollection<Usuario> coleccionUsuario;
        IMongoCollection<Usuario> ColeccionUsuario

        {
            get
            {
                if (coleccionUsuario == null)
                {
                    string connectionString =
     @"mongodb://appturismo:0hSQ4nkxAj325uSDCe4QRmCj9czKA4jHymyvt5XIZrd4g4Tr38vk549MnftCB1nHA8EE1G4PxqeAVBjL8BWq5A==@appturismo.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@appturismo@";
                    MongoClientSettings settings = MongoClientSettings.FromUrl(
                      new MongoUrl(connectionString)
                    );
                    settings.SslSettings =
                      new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                    var mongoClient = new MongoClient(settings);
                    //var client = new MongoClient(conec);
                    var db = mongoClient.GetDatabase(bdname);
                    var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
                    coleccionUsuario = db.GetCollection<Usuario>("Usuario", collectionSettings);





                }

                return coleccionUsuario;
            }

        }


        public async Task<Usuario> listaUsuario( string Email, string Pass)
        {
            try
            {
                var lista = ColeccionUsuario.AsQueryable<Usuario>().Where(u => u.email.Equals(Email) && u.passwordU.Equals(Pass)).SingleOrDefault();
                
                Console.WriteLine(" HOLA "+lista);

                return lista;
            }
            catch (Exception e)
            {
                Console.WriteLine("NO SE PUDO CAPTURAR LOS DATOS : " + e.Message);
            }
            return null;
        }



        public MongoClient clientemongo
        {
            get {
                string connectionString =
      @"mongodb://appturismo:0hSQ4nkxAj325uSDCe4QRmCj9czKA4jHymyvt5XIZrd4g4Tr38vk549MnftCB1nHA8EE1G4PxqeAVBjL8BWq5A==@appturismo.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@appturismo@";
                MongoClientSettings settings = MongoClientSettings.FromUrl(
                  new MongoUrl(connectionString)
                );
                settings.SslSettings =
                  new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                var mongoClient = new MongoClient(settings);

                return mongoClient;
            }
        }


        public async Task<IEnumerable<Entidad>> listaEntidades()
        {
            try
            {
                var lista = await ColeccionEntidades
                .Find(new BsonDocument())
                .ToListAsync();
                Console.WriteLine(lista);

                return lista;
            }
            catch (Exception e)
            {
                Console.WriteLine("NO SE PUDO CAPTURAR LOS DATOS : " + e.Message);
            }
            return null;
        }

        public async Task<IEnumerable<Categoria>> listaCategoria()
        {
            try
            {
                var lista = await ColeccionCategoria
                .Find(new BsonDocument())
                .ToListAsync();
                Console.WriteLine(lista);

                return lista;
            }
            catch (Exception e)
            {
                Console.WriteLine("NO SE PUDO CAPTURAR LOS DATOS : " + e.Message);
            }
            return null;
        }



        public async Task<IEnumerable<Entidad>> listaEntidadesSeleccionada( string id)
        {
            try
            {
                var entidadId = new ObjectId(id);
                var lista = ColeccionEntidades.AsQueryable<Entidad>().Where(e => e.id == id).ToList();
                return lista;
            }
            catch (Exception e)
            {
                Console.WriteLine("NO SE PUDO CAPTURAR LOS DATOS : " + e.Message);
            }
            return null;

        }

        public async Task<IEnumerable<Entidad>> CategoriaSeleccionada(string categoria)
        {
            try
            {
                
                var lista = ColeccionEntidades.AsQueryable<Entidad>().Where(e => e.categoria == categoria).ToList();
                return lista;
            }
            catch (Exception e)
            {
                Console.WriteLine("NO SE PUDO CAPTURAR LOS DATOS : " + e.Message);
            }
            return null;

        }

        public async Task<IEnumerable<Direccion>> ListaDirecciones(string nombreentidad)
        {
            try
            {

                var lista = ColeccionDireccion.AsQueryable<Direccion>().Where(e => e.entidad == nombreentidad).ToList();
                return lista;
            }
            catch (Exception e)
            {
                Console.WriteLine("NO SE PUDO CAPTURAR LOS DATOS : " + e.Message);
            }
            return null;

        }
        public async Task<Usuario> listaUsu(string email)
        {
            var lista = ColeccionUsuario.AsQueryable<Usuario>().Where(u => u.email.Equals(email)).SingleOrDefault();

            try
            {
                //var lista = ColeccionUsuario.AsQueryable<Usuario>().Where(u => u.email.Equals(email)).SingleOrDefault();

                Console.WriteLine(" HOLA " + lista);

                return lista;
            }
            catch (Exception e)
            {
                Console.WriteLine("NO SE PUDO CAPTURAR LOS DATOS : " + e.Message);
            }
            return lista;
        }

    
        public async Task UpdateEntidad(Entidad entidad)
        {
            await ColeccionEntidades.ReplaceOneAsync( e => e.id.Equals(entidad.id),entidad);
        }

        
        public async  Task InsertarRegistro(Usuario user)
        {
            
                     await ColeccionUsuario.InsertOneAsync(user);
                    
                
              
           
            
            //await ColeccionUsuario.InsertOneAsync(user);
            //return listau;



          
 
            
        }

        public async Task RecuperarPass( string Email, string urlBase, string prefijo, string controlador )
        {
            var emailusuario = new UserRequest {Email= Email, };
            var request = JsonConvert.SerializeObject(emailusuario);
            var content = new StringContent( request, Encoding.UTF8,"application/json");
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(urlBase);
            var url = string.Format("{0}{1}", prefijo, controlador);
            var respuestaNav = await cliente.PostAsync(url, content);
        }


    }
}
