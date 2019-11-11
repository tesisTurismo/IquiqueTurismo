using appTurismoIqq.Helpers;
using appTurismoIqq.Modelo;
using appTurismoIqq.Servicios;
using appTurismoIqq.Vistas;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace appTurismoIqq.VistaModelo
{
    public class CategoriasVModel:BaseVModelo
    {
        ApiServicio apiServicio;
        private bool isRefreshing;

        public List<Categoria> MyCategorias { get; set; }

        private List<CategoriaItemVModel> listacategorias;
        public List<CategoriaItemVModel> ListaCategorias
        {
            get { return this.listacategorias; }
            set { this.SetValue(ref this.listacategorias, value); }

        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }

        }
        private string nombreSeleccionado;
        public string NombreSeleccionado
        {
            get { return this.nombreSeleccionado; }
            set { this.SetValue(ref this.nombreSeleccionado, value); }

        }
        private static CategoriasVModel instancia;
        public CategoriasVModel()
        {
            instancia = this;
            this.apiServicio = new ApiServicio();
            LoadCategorias();


        }


        string lenguaje = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        public void RefreshList()
        {

            if (lenguaje == "es")
            {
                var mylistaNVM = this.MyCategorias.Select(p => new CategoriaItemVModel
                {

                    id = p.id,
                    fotoCategoria = p.fotoCategoria,
                    nombre = p.nombre,
                    nombreEng = p.nombreEng

                });

                this.ListaCategorias = new List<CategoriaItemVModel>(
                    mylistaNVM.OrderBy(p => p.nombre));
            }
            if (lenguaje == "en")
            {
                var mylistaNVM = this.MyCategorias.Select(p => new CategoriaItemVModel
                {

                    id = p.id,
                    fotoCategoria = p.fotoCategoria,
                    nombre = p.nombreEng,
                    nombreEng = p.nombreEng

                });

                this.ListaCategorias = new List<CategoriaItemVModel>(
                    mylistaNVM.OrderBy(p => p.nombre));
            }


        }

        //métodos
        private async void LoadCategorias()
        {
            try
            {
                this.IsRefreshing = true;
                var connection = await this.apiServicio.ValidacionInternet();
                if (!connection.respExitosa)
                {
                    this.IsRefreshing = false;
                    await Application.Current.MainPage.DisplayAlert("Error", connection.mensaje,"Aceptar");
                    return;
                }
                // var mongoService = new ApiServicio();
                MyCategorias = (List<Categoria>)await apiServicio.listaCategoria();
                RefreshList();
                this.IsRefreshing = false;
            }
            catch (Exception e)
            {
                Console.WriteLine("NO SE PUDO TOMAR LOS DATOSSSSSSS : " + e.Message);
            }

        }

        //comandos por ejecular al hacer click..
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadCategorias);
            }
        }


        public ICommand CerrarCommand
        {
            get
            {
                return new RelayCommand(Cerrar);
            }

        }

        private void Cerrar()
        {
            Settings.IsRemembered = false;
            VistaPrincipal.GetInstancia().Login = new LoginVModelo();
            App.Navigator.PushAsync(new LoginPage());
        }

    }
}
