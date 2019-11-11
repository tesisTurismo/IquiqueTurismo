using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace appTurismo.Web.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {

            if (Session["sesion"] == null)
            {
                return Redirect("~/Home/Index?mensaje=1");
            }
            else
            {
                Content("Bienvenido " + Session["sesion"]);
            }
            return View();

        }

        
        public ActionResult cerrarsesion()
        {
            Session.Remove("sesion");
            return RedirectToAction("Index", "Home");
           
        }
      
    }
}