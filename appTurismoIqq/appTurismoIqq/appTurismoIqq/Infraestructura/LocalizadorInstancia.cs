using appTurismoIqq.VistaModelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace appTurismoIqq.Infraestructura
{
    public class LocalizadorInstancia
    {
        public VistaPrincipal Main { get; set; }

        public LocalizadorInstancia()
        {
            this.Main = new VistaPrincipal();
        }
    }
}
