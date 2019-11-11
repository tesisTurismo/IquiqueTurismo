using appTurismoIqq.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace appTurismoIqq.VistaModelo
{
    public class VistaPrincipal
    {
        public EntidadesVModelo Entidades { get; set; }
        public DetalleEntidadVModelo detalleEntidad { get; set; }
        public CategoriasVModel Categorias { get; set; }
        public LoginVModelo Login { get; set; }
        public RegistroVModel Registros { get; set; }
        public DetalleDireccionVModelo detalleDireccion { get; set; }
        public RecuperarClaveVModelo Recuperacion { get; set; }
        public ObservableCollection<MenuItemVModelo> Menus { get; set; }
        public VistaPrincipal()
        {
            //this.Entidades = new EntidadesVModelo();
            instancia = this;
            this.LoadMenus();

        }

        private static VistaPrincipal instancia;

        public static VistaPrincipal GetInstancia()
        {
            if (instancia == null)
            {
                return new VistaPrincipal();
            }
            return instancia;
        }

        private void LoadMenus()
        {
            var menus = new List<Menu>
        {
            new Menu
            {
                Icono = "ic_card_giftcard",
                Pagina = "DescuentoPage",
                Titulo = "Descuento"
            },

           

            new Menu
            {
                Icono = "ic_exit_to_app",
                Pagina = "LoginPage",
                Titulo = "Cerrar sesión"
            }
        };

            this.Menus = new ObservableCollection<MenuItemVModelo>(
                menus.Select(m => new MenuItemVModelo
                {
                    Icono = m.Icono,
                    Pagina = m.Pagina,
                    Titulo = m.Titulo
                }).ToList());
        }
    }
}
