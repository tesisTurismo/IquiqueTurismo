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
    public class RegistroVModel : BaseVModelo
    {

        private ApiServicio apiServicio = new ApiServicio();

        //private ImageSource imageSource;
        private bool isRunning;
        private bool isEnabled;
        //private bool isRefreshing;
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Contraseña { get; set; }
        public string ConfirmarContraseña { get; set; }
        public string PaisOrigen { get; set; }
        public string CiudadOrigen { get; set; }
        public string Nacionalidad { get; set; }
        public string Email { get; set; }

        public bool IsRunning
        {

            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }

        /*public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { this.SetValue(ref this.imageSource, value); }
        } */

        public RegistroVModel()
        {
            this.apiServicio = new ApiServicio();
            this.IsEnabled = true;
            // this.ImageSource = "nouser";*/
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Guardar);
            }
        }

        private async void Guardar()
        {
            this.IsRunning = true;
            this.IsEnabled = false;
            if (string.IsNullOrEmpty(this.Nombre))
            {
                await Application.Current.MainPage.DisplayAlert(
                      "Error",
                    "Debe ingresar su Nombre",
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.Apellido))
            {
                await Application.Current.MainPage.DisplayAlert(
                     "Error",
                    "Debe ingresar su Apellido",
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.Contraseña))
            {
                await Application.Current.MainPage.DisplayAlert(
                     "Error",
                    "Debe ingresar su Contraseña",
                    "Aceptar");
                return;
            }

            if (this.Contraseña.Length < 6)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe ingresar como minimo 6 caracteres",
                    "Aceptar");
                return;
            }


            if (string.IsNullOrEmpty(this.ConfirmarContraseña))
            {
                await Application.Current.MainPage.DisplayAlert(
                     "Error",
                    "La contraseña debe ser igual a la anterior",
                    "Aceptar");
                return;
            }


            if (!this.Contraseña.Equals(this.ConfirmarContraseña))
            {
                await Application.Current.MainPage.DisplayAlert(
                     "Error",
                    "La contraseña no coinsiden",
                    "Aceptar");
                return;
            }


            if (string.IsNullOrEmpty(this.PaisOrigen))
            {
                await Application.Current.MainPage.DisplayAlert(
                     "Error",
                    "Debe ingresar su País de Origen",
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.CiudadOrigen))
            {
                await Application.Current.MainPage.DisplayAlert(
                     "Error",
                    "Debe ingresar su Ciudad de Origen",
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.Nacionalidad))
            {
                await Application.Current.MainPage.DisplayAlert(
                     "Error",
                    "Debe ingresar su Nacionalidad",
                    "Aceptar");
                return;
            }


            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                     "Error",
                    "Debe ingresar su Email",
                    "Aceptar");
                return;
            }

            if (!RegexHelper.IsValidEmailAddress(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe ingresar un correo verdadero",
                    "Aceptar");
                return;
            }
           
            var DatosUsu = new Usuario
            {

                nombreU = Nombre,
                apellidoU = Apellido,
                passwordU = Contraseña,
                paisOrigen = PaisOrigen,
                ciudadOrigen = CiudadOrigen,
                nacionalidad = Nacionalidad,
                email = Email,


            };

            try
            {
                var connection = await this.apiServicio.ValidacionInternet();
                if (!connection.respExitosa)
                {
                    this.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert("Error", connection.mensaje, "Aceptar");
                    return;
                }

                Usuario usua = await apiServicio.listaUsu(this.Email);


                if (usua != null)
                {
                    this.IsRunning = false;
                    this.isEnabled = true;

                    await Application.Current.MainPage.DisplayAlert(
                    "Email Existente",
                    "Este email ya esta en uso",
                    "Aceptar");
                    

                }
                else
                {
                    if (usua == null)
                    {
                        await apiServicio.InsertarRegistro(DatosUsu);
                        await Application.Current.MainPage.DisplayAlert(
                                        "Mensaje ",
                                        "Usuario registrado exitosamente",
                                        "Aceptar");
                        this.IsRunning = false;
                        this.isEnabled = true;
                        VistaPrincipal.GetInstancia().Login = new LoginVModelo();
                        await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
                    }

                }

            }
            catch(Exception e)
            {
                Console.WriteLine("HUBO UNA EXCEPTION EN registro de usuario: " + e.Message);
            }



        } //Cierre metodo Guardar








































    } //Cierre de la clase RegistroVModel
} //Cierre del namespace
