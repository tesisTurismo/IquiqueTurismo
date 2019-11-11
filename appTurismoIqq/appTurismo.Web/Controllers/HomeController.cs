using appTurismoIqq.Modelo;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;

namespace appTurismo.Web.Controllers
{
    public class HomeController : Controller
    {
        string connectionString =

@"mongodb://appturismo:0hSQ4nkxAj325uSDCe4QRmCj9czKA4jHymyvt5XIZrd4g4Tr38vk549MnftCB1nHA8EE1G4PxqeAVBjL8BWq5A==@appturismo.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@appturismo@";
        string bdname = "bdTurismo";
        [HttpGet]
        public ActionResult Index()
        {

            if (Request.Params["mensaje"]!=null)
            {
                string mensaje = Request.Params["mensaje"];
                if (mensaje=="1")
                {
                    TempData["msg"] = "<script>alert('Debe iniciar sesión');</script>";
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index( string user, string pass)
        {
            try
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

                

                // var cliente = new MongoClient(conec);

                //var database = cliente.GetDatabase(bdname);
                var lista = db.GetCollection<Administrador>("Administrador").AsQueryable<Administrador>().Where(u => u.nombreAdmin.Equals(user) && u.password.Equals(pass)).SingleOrDefault();
                if (lista != null)
                {
                    Session["sesion"]=user;

                    return RedirectToAction("Index", "Menu");
                }
                else
                {
                    Response.Redirect("Index");
                }


                return RedirectToAction("Index");


            }
            catch (Exception e)
            {
                return ViewBag.Error = e.Message;
            }
        }



    }
}