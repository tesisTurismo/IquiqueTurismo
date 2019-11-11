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
    public class DetalleEntidadItemVModelo : Direccion
    {
        private ApiServicio apiservicio;
        public DetalleEntidadItemVModelo()
        {
            this.apiservicio = new ApiServicio();
        }


        public ICommand DetalleDireccionCommand
        {
            get
            {
                return new RelayCommand(IrDetalleDireccion);
            }
        }

        private async void IrDetalleDireccion()
        {
            VistaPrincipal.GetInstancia().detalleDireccion = new DetalleDireccionVModelo(this);
            await App.Navigator.PushAsync(new DetalleDireccionPage());
        }
    }
}