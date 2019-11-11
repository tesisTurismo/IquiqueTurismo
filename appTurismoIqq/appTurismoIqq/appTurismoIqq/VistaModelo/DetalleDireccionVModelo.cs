using appTurismoIqq.Geolocalizacion;
using appTurismoIqq.Modelo;
using appTurismoIqq.Servicios;
using appTurismoIqq.Vistas;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace appTurismoIqq.VistaModelo
{
    public class DetalleDireccionVModelo : BaseVModelo
    {
        private Direccion direccion;
        private string direccionLugar;
        private string telefono;
        public double latitud2;
        public string calle;
        public double longitud2;
        private double latitud;
        private double longitud;
        private ApiServicio apiServicio;
        public Direccion Direccion
        {
            get { return this.direccion; }
            set { this.SetValue(ref this.direccion, value); }
        }
        public string DireccionLugar
        {
            get { return this.direccionLugar; }
            set { this.SetValue(ref this.direccionLugar, value); }
        }
        public string Telefono
        {
            get { return this.telefono; }
            set { this.SetValue(ref this.telefono, value); }
        }
        public double Latitud
        {
            get { return this.latitud; }
            set { this.SetValue(ref this.latitud, value); }
        }
        public double Longitud
        {
            get { return this.longitud; }
            set { this.SetValue(ref this.longitud, value); }
        }
        public DetalleDireccionVModelo(Direccion direccion)
        {
            this.Direccion = direccion;
            this.apiServicio = new ApiServicio();
            this.DireccionLugar = direccion.direccion;
            this.Telefono = direccion.telefono;
            this.Latitud = direccion.latitud;
            this.Longitud = direccion.longitud;
            latitud2 = latitud;
            longitud2 = longitud;
            calle = this.direccion.direccion;

        }

        public ICommand MapaCommand2
        {
            get
            {
                return new RelayCommand(IrMapa2);
            }
        }

        private async void IrMapa2()
        {

            await App.Navigator.PushAsync(new MapAppPage2(latitud2, longitud2, calle));

        }
    }
}