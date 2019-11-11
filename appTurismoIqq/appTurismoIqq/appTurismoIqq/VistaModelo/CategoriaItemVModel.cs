using appTurismoIqq.Helpers;
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
    public class CategoriaItemVModel:Categoria
    {
        private ApiServicio apiservicio;
        public CategoriaItemVModel()
        {
            this.apiservicio = new ApiServicio();
        }
        public ICommand CategoriaCommand
        {
            get
            {
                return new RelayCommand(IrCategoria);
            }
        }

        private async void IrCategoria()
        {

            VistaPrincipal.GetInstancia().Entidades= new EntidadesVModelo(this);
            await App.Navigator.PushAsync(new EntidadesPage());

        }
    }
}
