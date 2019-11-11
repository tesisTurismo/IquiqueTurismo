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
    public class EntidadesItemVModel:Entidad
    {
        private ApiServicio apiservicio;
        private Entidad entidad { get; set; }

        public EntidadesItemVModel()
        {
            this.apiservicio = new ApiServicio();
        }

        public ICommand DetalleEntidadCommand
        {
            get
            {
                return new RelayCommand(IrDetalleEntidad);
            }
        }

        private async void IrDetalleEntidad()
        {


            
            VistaPrincipal.GetInstancia().detalleEntidad = new DetalleEntidadVModelo(this);

            await App.Navigator.PushAsync(new DetalleEntidadPage());



        }
    }
}
