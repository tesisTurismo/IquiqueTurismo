using appTurismoIqq.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appTurismo.Web.Models
{
    public class EntidadVista : Entidad
    {
        public HttpPostedFileBase fotoFile { get; set; }
    }
}