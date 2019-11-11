using appTurismo.Web.App_Start;
using appTurismoIqq.Modelo;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using appTurismoIqq.Helpers;
using System.Threading.Tasks;

namespace appTurismo.Web.Controllers
{
    public class UsuarioController : Controller
    {
        string conec = "mongodb+srv://user:tesis123456@tesis1-7onzc.azure.mongodb.net/test";
        string bdname = "bdTurismo";

        string connectionString =

 @"mongodb://appturismo:0hSQ4nkxAj325uSDCe4QRmCj9czKA4jHymyvt5XIZrd4g4Tr38vk549MnftCB1nHA8EE1G4PxqeAVBjL8BWq5A==@appturismo.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@appturismo@";

        // GET: Usuario
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
            var listaUsuarios = db.GetCollection<Usuario>("Usuario").Find(new BsonDocument()).ToList();

            if (Session["sesion"] == null)
            {
                return Redirect("~/Home/Index?mensaje=1");
            }
            else
            {
                Content("Bienvenido " + Session["sesion"]);
            }

            return View(listaUsuarios);
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
       
        // POST: Usuario/Envio/
       
        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
