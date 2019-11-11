using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using appTurismoIqq.Helpers;
using appTurismoIqq.Servicios;
using appTurismoIqq.VistaModelo;
using GalaSoft.MvvmLight.Command;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;


/*
  En esta clase se instancia el mapa de google para su utilización, además de manejar la geolocalización para mostrar ubicaciones
  y direcciones

  Al principio se utilizaba un botón para señalar la posición y poner los pines, luego se desecho esa idea por poner la posición
  del usuario inmediatamente y con un pin la posición del lugar buscado

  Se están usando valores estaticos de momento, hasta que se puedan usar los almacenados en la base de datos

    */
namespace appTurismoIqq.Geolocalizacion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapAppPage2 : ContentPage

    {
        public string boton;
        double lat;
        double lon;

        public MapAppPage2(double lat, double lon, string calle)
        {
            InitializeComponent();

            this.lat = lat;
            this.lon = lon;
            
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(-20.247508, -70.133661), Distance.FromMiles(10)).WithZoom(9));

            //Declaración de una variable tipo pin
            var pin3 = new Pin()
            {
                Label = "Ubicación",
                Position = new Position(lat, lon), //Asignando valor a la posición del pin (latitud, longuitud)
                Address = calle,
            };

            map.MyLocationEnabled = true; 
            map.Pins.Add(pin3); // Agregar la variable instanciada pin a la variable map

            traceRouteButton.Clicked += TraceRouteButton_Clicked;
        }

        private void TraceRouteButton_Clicked(object sender, System.EventArgs e)
        {
            
            Task.Run(async () =>
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);
                var result = await GoogleDirectionsClient.GetDirections(
                    new Location()
                    {
                        Latitude = lat,
                        Longitude = lon
                    },
                    new Location()
                    {
                        Latitude = location.Latitude,
                        Longitude = location.Longitude,
                    },
                    new List<Location>()
                );

                if (this.map != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        GoogleMapsDirectionsHelper.SetRoutes(this.map, result);
                    });
                }
            });
        }
    }
}