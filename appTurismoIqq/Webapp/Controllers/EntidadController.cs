using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webapp.App_Start;
using appTurismoIqq.Modelo;
using Webapp.Models;
using Webapp.Helpers;
using System.Threading.Tasks;

namespace Webapp.Controllers
{
    public class EntidadController : Controller
    {
        private MongoContext db;
        private IMongoCollection<Entidad> entidadesCollection;

        public EntidadController()
        {
            db = new MongoContext();
            entidadesCollection = db.bd.GetCollection<Entidad>("Entidad");
        }

        // GET: Entidad
        public ActionResult Index()
        {
            List<Entidad> entidades = entidadesCollection.AsQueryable<Entidad>().ToList();
            return View(entidades);
        }

        // GET: Entidad/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Entidad/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Entidad/Create
        [HttpPost]
        public ActionResult Create(Entidad entidad)
        {
            try
            {
             /*   
                    var pic = string.Empty;
                    var folder = "~/Content/ImagenesEntidades";

                    if (entidadvista.fotoFile != null)
                    {
                        pic = FilesHelper.UploadPhoto(entidadvista.fotoFile, folder);
                        pic = $"{folder}/{pic}";
                    }
                    //almaceno los datos en la variable local
                    var entidad = this.ToEntidad(entidadvista, pic);

       
    */

                    entidadesCollection.InsertOne(entidad);
                    // TODO: Add insert logic here

                    return RedirectToAction("Index");
                
            }
            catch
            {
                return View();
            }
        }

        private Entidad ToEntidad(EntidadView vistaEntidad, string pic)
        {
            return new Entidad
            {

                id = vistaEntidad.id,
                foto = pic,
                nombre = vistaEntidad.nombre,
                pagWeb = vistaEntidad.pagWeb,
                descripcion = vistaEntidad.descripcion,
                descripcionEng = vistaEntidad.descripcionEng,
                telefono= vistaEntidad.telefono,
                direccion=vistaEntidad.direccion,
                latitud=vistaEntidad.latitud,
                longitud=vistaEntidad.longitud,
                categoria = vistaEntidad.categoria,
            };
        }

        // GET: Entidad/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Entidad/Edit/5
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

        // GET: Entidad/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Entidad/Delete/5
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
