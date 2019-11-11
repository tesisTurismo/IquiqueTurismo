using appTurismo.Web.Models;
using appTurismoIqq.Modelo;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;

namespace appTurismo.Web.Controllers
{
    public class DireccionController : Controller
    {
        string bdname = "bdTurismo";
        string connectionString =

@"mongodb://appturismo:0hSQ4nkxAj325uSDCe4QRmCj9czKA4jHymyvt5XIZrd4g4Tr38vk549MnftCB1nHA8EE1G4PxqeAVBjL8BWq5A==@appturismo.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@appturismo@";
        // GET: Direccion
        public ActionResult Index()
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(
      new MongoUrl(connectionString));

            settings.SslSettings =
                         new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            //var client = new MongoClient(conec);
            var db = mongoClient.GetDatabase(bdname);
            var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
            var listaDirecciones = db.GetCollection<Direccion>("Direccion").Find(new BsonDocument()).ToList();
            if (Session["sesion"] == null)
            {
                return Redirect("~/Home/Index?mensaje=1");
            }
            else
            {
                Content("Bienvenido " + Session["sesion"]);
            }
            return View(listaDirecciones);
        }

        // GET: Direccion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Direccion/Create
        public ActionResult Create()
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(
         new MongoUrl(connectionString)
       );
            settings.SslSettings =
                          new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            //var client = new MongoClient(conec);
            var db = mongoClient.GetDatabase(bdname);
            var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
            var listaEntidad = db.GetCollection<BsonDocument>("Entidad");
            var filtro = Builders<BsonDocument>.Filter.Empty;
            var cursor = listaEntidad.Find(filtro);
            var listado = cursor.ToList();
            var lista = listado.Select(item => new Entidad()
            {
                id = item["_id"].ToString(),
                nombre = item["nombre"].ToString()
            }).ToList();
            ViewBag.entidad = new SelectList(lista, "nombre", "nombre");
            return View();
        }

        // POST: Direccion/Create
        [HttpPost]
        public ActionResult Create(Direccion direcciones)
        {
            try
            {
               // var direcciones = this.ToDireccion(direccion);

                // comienza la conexión
                MongoClientSettings settings = MongoClientSettings.FromUrl(
          new MongoUrl(connectionString)
        );
                settings.SslSettings =
                              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                var mongoClient = new MongoClient(settings);
                //var client = new MongoClient(conec);
                var db = mongoClient.GetDatabase(bdname);  //termina la conexión
                var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };

                var listaDirecciones = db.GetCollection<Direccion>("Direccion");
                var listaEntidades = db.GetCollection<BsonDocument>("Entidad");
                var filtro = Builders<BsonDocument>.Filter.Empty;
                var cursor = listaEntidades.Find(filtro);
                var listado = cursor.ToList();
                var lista = listado.Select(item => new Entidad()
                {
                    id = item["_id"].ToString(),
                    nombre = item["nombre"].ToString()
                }).ToList();
                ViewBag.entidad = new SelectList(lista, "nombre", "nombre", direcciones.entidad);

                listaDirecciones.InsertOneAsync(direcciones);
                //Direccion direc = new Direccion();


               // direc.comboentidad =(IEnumerable < Entidad > )lista;

                //return View(direc);
               return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();

            }
        }

        private Direccion ToDireccion(Direccion direccion)
        {
            return new Direccion
            {

                id = direccion.id,
                direccion = direccion.direccion,
                latitud = direccion.latitud,
                longitud = direccion.longitud,
                entidad = direccion.entidad,
                telefono = direccion.telefono,

            };
        }

        // GET: Direccion/Edit/5
        public ActionResult Edit(string id)
        {
            // TODO: Add update logic here
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Inicio de conexión
            MongoClientSettings settings = MongoClientSettings.FromUrl(
          new MongoUrl(connectionString)
        );
            settings.SslSettings =
                          new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            var db = mongoClient.GetDatabase(bdname); // Fin de conexión

            // Encontrar la entidad segun el id
            var direccion = db.GetCollection<Direccion>("Direccion").Find(new BsonDocument()).ToList().AsQueryable<Direccion>().SingleOrDefault(x => x.id.Equals(id));
            if (direccion == null)
            {
                return HttpNotFound();
            }

            //LLENADO DE COMBOBOX DROPDOWNLIST
            var listaentidades = db.GetCollection<BsonDocument>("Entidad");
            var filtro = Builders<BsonDocument>.Filter.Empty;
            var cursor = listaentidades.Find(filtro);
            var listado = cursor.ToList();
            var lista = listado.Select(item => new Entidad()
            {
                id = item["_id"].ToString(),
                nombre = item["nombre"].ToString()
            }).ToList();

            //Se asigna los valores al VIWEBAG QUE ESTA EN LA VISTA DE EDITAR
            ViewBag.identidad = new SelectList(lista, "nombre", "nombre");
           
            return View(direccion);
        }

        // POST: Direccion/Edit/5
        [HttpPost]
        public ActionResult Edit(Direccion direcciones  )
        {
            // TODO: Add update logic here
            try
            {
                if (ModelState.IsValid)
                {
                    MongoClientSettings settings = MongoClientSettings.FromUrl(
              new MongoUrl(connectionString)
            );
                    settings.SslSettings =
                                  new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                    var mongoClient = new MongoClient(settings);
                    var db = mongoClient.GetDatabase(bdname);  // Fin de la conexión
                    var listadirecciones = db.GetCollection<Direccion>("Direccion");

                    listadirecciones.FindOneAndUpdateAsync(Builders<Direccion>.Filter.Eq("id", direcciones.id), Builders<Direccion>.Update.Set("direccion", direcciones.direccion).Set("latitud", direcciones.latitud).Set("longitud", direcciones.longitud).
                   Set("entidad", direcciones.entidad).Set("telefono", direcciones.telefono));

                    var listaentidades = db.GetCollection<BsonDocument>("Entidad");
                    var filtros = Builders<BsonDocument>.Filter.Empty;
                    var cursor = listaentidades.Find(filtros);
                    var listado = cursor.ToList();
                    var lista = listado.Select(item => new Entidad()
                    {
                        id = item["_id"].ToString(),
                        nombre = item["nombre"].ToString()
                    }).ToList();
                    ViewBag.identidad = new SelectList(lista, "nombre", "nombre", direcciones.entidad);

                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Direccion/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Inicio de conexión a base de datos
            MongoClientSettings settings = MongoClientSettings.FromUrl(
          new MongoUrl(connectionString)
        );
            settings.SslSettings =
                          new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            var db = mongoClient.GetDatabase(bdname); // Fin de conexión

            var direccion = db.GetCollection<Direccion>("Direccion").Find(new BsonDocument()).ToList().AsQueryable<Direccion>().SingleOrDefault(x => x.id == id);
            if (direccion == null)
            {
                return HttpNotFound();
            }


            return View(direccion);
        }

        // POST: Direccion/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                // TODO: Add delete logic here

                if (ModelState.IsValid)
                {
                    // Inicio de conexión a base de datos
                    MongoClientSettings settings = MongoClientSettings.FromUrl(
          new MongoUrl(connectionString)
        );
                    settings.SslSettings =
                                  new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                    var mongoClient = new MongoClient(settings);
                    var db = mongoClient.GetDatabase(bdname); // Fin de la conexión
                    var listadirecciones = db.GetCollection<Direccion>("Direccion");

                    var DeleteRecord = listadirecciones.DeleteOneAsync(
                        Builders<Direccion>.Filter.Eq("id", id));
                    return RedirectToAction("Index");

                }
                return View();
            }
                
            catch
            {
                return View();
            }
        }
    }
}
