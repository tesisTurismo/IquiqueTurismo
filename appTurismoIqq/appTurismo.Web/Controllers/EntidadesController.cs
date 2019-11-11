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
    public class EntidadesController : Controller
    {
        string conec = "mongodb+srv://user:tesis123456@tesis1-7onzc.azure.mongodb.net/test";
        string bdname = "bdTurismo";

        string connectionString =

 @"mongodb://appturismo:0hSQ4nkxAj325uSDCe4QRmCj9czKA4jHymyvt5XIZrd4g4Tr38vk549MnftCB1nHA8EE1G4PxqeAVBjL8BWq5A==@appturismo.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@appturismo@";

        // GET: Entidades Create
        public ActionResult Index()
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
            var listaentidades = db.GetCollection<Entidad>("Entidad").Find(new BsonDocument()).ToList();

            if (Session["sesion"] == null)
            {
                return Redirect("~/Home/Index?mensaje=1");
            }
            else
            {
               Content("Bienvenido " + Session["sesion"]);
            }

            return View(listaentidades);
        }

        // GET: Entidades/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Entidades/Create
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
            var listacategorias = db.GetCollection<BsonDocument>("Categoria");
            var filtro = Builders<BsonDocument>.Filter.Empty;
            var cursor = listacategorias.Find(filtro);
            var listado = cursor.ToList();
            var lista = listado.Select(item => new Categoria()
            {
                id = item["_id"].ToString(),
                nombre = item["nombre"].ToString()
            }).ToList();
            ViewBag.categoria = new SelectList(lista, "nombre", "nombre");
            return View();
        }

        // POST: Entidades/Create
        [HttpPost]
        public ActionResult Create(EntidadVista entidadview)
        {
            try
            {
                var pic = string.Empty;
                var folder = "~/Content/ImagenesEntidades";

                if (entidadview.fotoFile != null)
                {
                    pic = FilesHelpers.UploadPhoto(entidadview.fotoFile, folder);
                    pic = $"{folder}/{pic}";
                }
                //almaceno los datos en la variable local
                var entidad = this.ToEntidad(entidadview, pic);

                // comienza la conexión
                MongoClientSettings settings = MongoClientSettings.FromUrl(
          new MongoUrl(connectionString)
        );
                settings.SslSettings =
                              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                var mongoClient = new MongoClient(settings);
                var db = mongoClient.GetDatabase(bdname); // fin de la conexión
                var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
                var listaentidades = db.GetCollection<Entidad>("Entidad");

                var listacategorias = db.GetCollection<BsonDocument>("Categoria");
                var filtro = Builders<BsonDocument>.Filter.Empty;
                var cursor = listacategorias.Find(filtro);
                var listado = cursor.ToList(); 
                var lista = listado.Select(item => new Categoria()
                {
                    id= item["_id"].ToString(),
                    nombre = item["nombre"].ToString()
                }).ToList();
                ViewBag.categoria = new SelectList(lista, "nombre", "nombre", entidadview.categoria);

                listaentidades.InsertOneAsync(entidad);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        private Entidad ToEntidad(EntidadVista vistaEntidad, string pic)
        {
            return new Entidad
            {

                id = vistaEntidad.id,
                foto = pic,
                nombre = vistaEntidad.nombre,
                pagWeb = vistaEntidad.pagWeb,
                descripcion = vistaEntidad.descripcion,
                descripcionEng = vistaEntidad.descripcionEng,
               

                categoria = vistaEntidad.categoria,
                vistas = vistaEntidad.vistas
            };
        }
        // GET: Entidades/Edit/5
        public ActionResult Edit(string id)
        {
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
            //var client = new MongoClient(conec);
            var db = mongoClient.GetDatabase(bdname);

            //ENCONTRAR ENTIDAD SEGÚN EL ID QUE TIENE
            var entidad = db.GetCollection<Entidad>("Entidad").Find(new BsonDocument()).ToList().AsQueryable<Entidad>().SingleOrDefault(x => x.id.Equals(id));
            if (entidad==null)
            {
                return HttpNotFound();
            }
            //LLENADO DE COMBOBOX DROPDOWNLIST
            var listacategorias = db.GetCollection<BsonDocument>("Categoria");
            var filtro = Builders<BsonDocument>.Filter.Empty;
            var cursor = listacategorias.Find(filtro);
            var listado = cursor.ToList();
            var lista = listado.Select(item => new Categoria()
            {
                id = item["_id"].ToString(),
                nombre = item["nombre"].ToString()
            }).ToList();

            //Se asigna los valores al VIWEBAG QUE ESTA EN LA VISTA DE EDITAR
            ViewBag.idcategoria = new SelectList(lista, "nombre", "nombre");
            var view = this.ToView(entidad);
            return View(view);
        } // Fin del metodo GET Entidades Edit

        private EntidadVista ToView(Entidad ent)
        {
            return new EntidadVista
            {
                id = ent.id,
                foto = ent.foto,
                nombre = ent.nombre,
                pagWeb = ent.pagWeb,
                descripcion = ent.descripcion,
                descripcionEng = ent.descripcionEng,
               categoria = ent.categoria,
                vistas = ent.vistas
            };
        }


        private Entidad ToEntidades(EntidadVista vistaEntidad, string pic,string cat)
        {
            return new Entidad
            {

                id = vistaEntidad.id,
                foto = pic,
                nombre = vistaEntidad.nombre,
                pagWeb = vistaEntidad.pagWeb,
                descripcion = vistaEntidad.descripcion,
                descripcionEng = vistaEntidad.descripcionEng,
                categoria = cat,
                vistas = vistaEntidad.vistas
            };
        }
        // POST: Entidades/Edit/5
        [HttpPost]
        public ActionResult Edit( EntidadVista entidadview)

        {  
            // TODO: Add update logic here
            if (ModelState.IsValid)
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(
          new MongoUrl(connectionString)
        );
                settings.SslSettings =
                              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                var mongoClient = new MongoClient(settings);
                var db = mongoClient.GetDatabase(bdname);
                var listaentidades = db.GetCollection<Entidad>("Entidad");

                //FOTO 
                var pic = entidadview.foto;
                var folder = "~/Content/ImagenesEntidades";
                var categoriaselect = entidadview.categoria;

                if (entidadview.fotoFile != null)
                {
                    pic = FilesHelpers.UploadPhoto(entidadview.fotoFile, folder);
                    pic = $"{folder}/{pic}";
                }

                //almaceno los datos en la variable 
                var entidad = this.ToEntidades(entidadview, pic, categoriaselect);

                listaentidades.FindOneAndUpdateAsync(Builders<Entidad>.Filter.Eq("id", entidad.id), Builders<Entidad>.Update.Set("foto", entidad.foto).Set("nombre", entidad.nombre).Set("pagWeb", entidad.pagWeb).
                    Set("descripcion", entidad.descripcion).Set("descripcionEng", entidad.descripcionEng)
                    .Set("categoria", entidad.categoria).Set("vistas", entidad.vistas));

                var listacategorias = db.GetCollection<BsonDocument>("Categoria");
                var filtros = Builders<BsonDocument>.Filter.Empty;
                var cursor = listacategorias.Find(filtros);
                var listado = cursor.ToList();
                var lista = listado.Select(item => new Categoria()
                {
                    id = item["_id"].ToString(),
                    nombre = item["nombre"].ToString()
                }).ToList();


                ViewBag.idcategoria = new SelectList(lista, "nombre", "nombre", entidadview.categoria);
                //var result = listaentidades.FindOneAndUpdate(filtro,update);

                return RedirectToAction("Index");
            }



            return View(entidadview);

        }

        // GET: Entidades/Delete/5
        public ActionResult Delete(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MongoClientSettings settings = MongoClientSettings.FromUrl(
          new MongoUrl(connectionString)
        );
            settings.SslSettings =
                          new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            //var client = new MongoClient(conec);
            var db = mongoClient.GetDatabase(bdname);
            var entidad = db.GetCollection<Entidad>("Entidad").Find(new BsonDocument()).ToList().AsQueryable<Entidad>().SingleOrDefault(x => x.id == id);
            if (entidad == null)
            {
                return HttpNotFound();
            }
            return View(entidad);
        }

        // POST: Entidades/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(String id)
        {
            try
            {
                // TODO: Add delete logic here
                if (ModelState.IsValid)
                {
                    MongoClientSettings settings = MongoClientSettings.FromUrl(
          new MongoUrl(connectionString)
        );
                    settings.SslSettings =
                                  new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                    var mongoClient = new MongoClient(settings);
                    //var client = new MongoClient(conec);
                    var db = mongoClient.GetDatabase(bdname);
                    var listaentidades = db.GetCollection<Entidad>("Entidad");

                    var DeleteRecord = listaentidades.DeleteOneAsync(
                        Builders<Entidad>.Filter.Eq("id", id));
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
