using System;
using System.Collections.Generic;
using System.Text;

/* En esta clase se crea la interfaz para poder obtener la localización y manejar los aspectos que necesiten esto */

namespace appTurismoIqq.Geolocalizacion
{
    public interface ILocation
    {
        void ObtainMyLocation();
        event EventHandler<ILocationEventArgs> locationObtained;
    }
    public interface ILocationEventArgs
    {
        double lat { get; set; }
        double lng { get; set; }
    }
}
