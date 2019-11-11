using appTurismoIqq.Modelo;
using appTurismoIqq.Servicios;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using appTurismoIqq.Vistas;

namespace appTurismoIqq.VistaModelo
{
    public class EntidadesVModelo : BaseVModelo
    {
        ApiServicio apiEntidades;
        private bool isRefreshing;
        private string filtro;

        public List<Entidad> MyEntidades { get; set; }

        private List<EntidadesItemVModel> listaentidades;
        public List<EntidadesItemVModel> Listaentidades
        {
            get { return this.listaentidades; }
            set { this.SetValue(ref this.listaentidades,value); }

        }
        public Categoria Categoria { get; set; }
        
             public string Filtro
        {
            get { return this.filtro; }
            set
            {
                this.filtro = value;
                this.RefreshList();
            }
        }
    
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }

        }



        private static EntidadesVModelo instancia;
        public EntidadesVModelo(Categoria categoria)
        {
            instancia = this;

            this.Categoria = categoria;
            this.apiEntidades = new ApiServicio();

            LoadEntidades();
        }

      
        public void RefreshList()
        {
            if (string.IsNullOrEmpty(this.Filtro))
            {

                var mylistaNVM = this.MyEntidades.Select(p => new EntidadesItemVModel
                {
                    id = p.id,
                    foto = p.foto,
                    nombre = p.nombre,
                    pagWeb = p.pagWeb,
                    descripcion = p.descripcion,
                    descripcionEng = p.descripcionEng,

                    //telefono = p.telefono,

                    categoria = p.categoria,
                    vistas = p.vistas
                });
                this.Listaentidades = new List<EntidadesItemVModel>(
                    mylistaNVM.OrderBy(p => p.nombre));
            }
            else
            {
                var mylistaNVM = this.MyEntidades.Select(p => new EntidadesItemVModel
                {
                    id = p.id,
                    foto = p.foto,
                    nombre = p.nombre,
                    pagWeb = p.pagWeb,
                    descripcion = p.descripcion,
                    descripcionEng = p.descripcionEng,

                    //telefono = p.telefono,

                    categoria = p.categoria,
                    vistas = p.vistas
                }).Where(p =>p.nombre.ToLower().Contains(this.filtro.ToLower())).ToList();
                this.Listaentidades = new List<EntidadesItemVModel>(
                   mylistaNVM.OrderBy(p => p.nombre));
            }
            
        }
      

        //métodos
        private async void LoadEntidades()
        {
            try
            {
                this.IsRefreshing = true;
                // var mongoService = new ApiServicio();
                MyEntidades =  (List<Entidad>) await apiEntidades.CategoriaSeleccionada(Categoria.nombre);
                RefreshList();
                this.IsRefreshing = false;
            }
            catch (Exception e)
            {
                Console.WriteLine("NO SE PUDO TOMAR LOS DATOSSSSSSSVM : " + e.Message);
            }
            
        }

        //comandos por ejecular al hacer click..
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadEntidades);
            }
        }
        /*
        public async void mostrarentidades()
        {
            try
            {
               // Entidades.coleccionSeleccionada = "entidad";
                Listaentidades = await Entidades.Coleccion.Find(new BsonDocument()).ToListAsync();
                return ;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("No se pudo capturar los datos : "+e.Message);

            }
            return ;
        }
        */
        
    }
}
