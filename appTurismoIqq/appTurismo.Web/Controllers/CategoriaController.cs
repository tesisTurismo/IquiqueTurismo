using appTurismo.Web.Helpers;
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
    public class CategoriaController : Controller
    {
        string conec = "mongodb+srv://user:tesis123456@tesis1-7onzc.azure.mongodb.net/test";
        string bdname = "bdTurismo";

        string connectionString =

 @"mongodb://appturismo:0hSQ4nkxAj325uSDCe4QRmCj9czKA4jHymyvt5XIZrd4g4Tr38vk549MnftCB1nHA8EE1G4PxqeAVBjL8BWq5A==@appturismo.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@appturismo@";

        public CategoriaController()
        {

        }
        // GET: Categoria
        public ActionResult Index()
        {
            // Inicio de conexión a base de datos
            MongoClientSettings settings = MongoClientSettings.FromUrl(
         new MongoUrl(connectionString));

            settings.SslSettings =
                         new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            var db = mongoClient.GetDatabase(bdname); // Fin de la conexión
            var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
            var listaCategorias = db.GetCollection<Categoria>("Categoria").Find(new BsonDocument()).ToList();
            if (Session["sesion"] == null)
            {
                return Redirect("~/Home/Index?mensaje=1");
            }
            else
            {
                Content("Bienvenido " + Session["sesion"]);
            }
            return View(listaCategorias);
        }

        // GET: Categoria/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Categoria/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: Categoria/Create
        [HttpPost]
        public ActionResult Create(CategoriaVista categoriavista)
        {
            try
            {
                var pic = string.Empty;
                var folder = "~/Content/ImagenesCategorias";

                if (categoriavista.fotoFile != null)
                {
                    pic = FilesHelpers.UploadPhoto(categoriavista.fotoFile, folder);
                    pic = $"{folder}/{pic}";
                }
                var categoria = this.ToCategoria(categoriavista, pic);

                // comienza la conexión
                MongoClientSettings settings = MongoClientSettings.FromUrl(
          new MongoUrl(connectionString)
        );
                settings.SslSettings =
                              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                var mongoClient = new MongoClient(settings);
                var db = mongoClient.GetDatabase(bdname); // Final de la conexión
                var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
                var listacategorias = db.GetCollection<Categoria>("Categoria");

                listacategorias.InsertOneAsync(categoria);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private Categoria ToCategoria(CategoriaVista categoriavista, string pic)
        {
            return new Categoria
            {
                id = categoriavista.id,
                fotoCategoria = pic,
                nombre = categoriavista.nombre,
                nombreEng = categoriavista.nombreEng,
            };
        }
       

        // GET: Categoria/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Inicio de conexión a base de datos 
            MongoClientSettings settings = MongoClientSettings.FromUrl(
         new MongoUrl(connectionString)
       );
            settings.SslSettings =
                          new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            var db = mongoClient.GetDatabase(bdname); // Fin de la conexión
            var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
            
            var categoria = db.GetCollection<Categoria>("Categoria").Find(new BsonDocument()).ToList().AsQueryable<Categoria>().SingleOrDefault(x => x.id.Equals(id));
            if (categoria == null)
            {
                return HttpNotFound();
            }
            var view = this.ToView(categoria);
            return View(view);
        }

        private CategoriaVista ToView(Categoria categoria)
        {
            return new CategoriaVista
            {
                id = categoria.id,
                fotoCategoria = categoria.fotoCategoria,
                nombre = categoria.nombre,
                nombreEng = categoria.nombreEng,
            };
        }



        // POST: Categoria/Edit/5
        [HttpPost]
        public ActionResult Edit(CategoriaVista categoriavista)
        {
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
                    var db = mongoClient.GetDatabase(bdname);
                    var listacategorias = db.GetCollection<Categoria>("Categoria");

                    //FOTO 
                    var pic = categoriavista.fotoCategoria;
                    var folder = "~/Content/ImagenesCategorias";

                    if (categoriavista.fotoFile != null)
                    {
                        pic = FilesHelpers.UploadPhoto(categoriavista.fotoFile, folder);
                        pic = $"{folder}/{pic}";
                    }
                    //almaceno los datos en la variable local
                    var categoria = this.ToCategoria(categoriavista, pic);

                    listacategorias.FindOneAndUpdateAsync(Builders<Categoria>.Filter.Eq("id", categoria.id), Builders<Categoria>.Update.Set("fotoCategoria", categoria.fotoCategoria).Set("nombre", categoria.nombre).Set("nombreEng", categoria.nombreEng));

                    return RedirectToAction("Index");

                }

                
                
                return View(categoriavista);

            }
            catch
            {
                return View();
            }
        }

        // GET: Categoria/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Inicio de conexiión a base de datos
            MongoClientSettings settings = MongoClientSettings.FromUrl(
            new MongoUrl(connectionString)
            );
            settings.SslSettings =
                          new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            var db = mongoClient.GetDatabase(bdname); // Fin de la conexión
            var categoria = db.GetCollection<Categoria>("Categoria").Find(new BsonDocument()).ToList().AsQueryable<Categoria>().SingleOrDefault(x => x.id == id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        } // Fin del metodo GET Categoria Delete

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                // TODO: Add delete logic here
                if (ModelState.IsValid)
                {
                    // Inicio de la conexión a base de datos
                    MongoClientSettings settings = MongoClientSettings.FromUrl(
          new MongoUrl(connectionString)
        );
                    settings.SslSettings =
                                  new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                    var mongoClient = new MongoClient(settings);
                    var db = mongoClient.GetDatabase(bdname); // Fin de la conexión
                    var listaentidades = db.GetCollection<Categoria>("Categoria");

                    var DeleteRecord = listaentidades.DeleteOneAsync(
                        Builders<Categoria>.Filter.Eq("id", id));
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
