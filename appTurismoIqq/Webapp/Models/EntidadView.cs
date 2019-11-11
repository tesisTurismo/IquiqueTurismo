using appTurismoIqq.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webapp.Models
{
    public class EntidadView:Entidad
    {
        public HttpPostedFileBase fotoFile { get; set; }
    }
}