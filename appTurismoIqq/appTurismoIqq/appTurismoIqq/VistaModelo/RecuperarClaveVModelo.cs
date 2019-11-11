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
    public class RecuperarClaveVModelo : BaseVModelo
    {
        ApiServicio apiServicios;
        private string email;
        private bool isRunning;
        private bool isEnabled;
        public String Email
        {
            get { return this.email; }
            set { this.SetValue(ref this.email, value); }
        }
        public RecuperarClaveVModelo()
        {
            apiServicios = new ApiServicio();
            this.IsEnabled = true;
        }
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
        public ICommand EnviarCommand
        {
            get
            {
                return new RelayCommand(Enviar);
            }
        }

        private async void Enviar()
        {
            this.IsRunning = true;
            this.IsEnabled = false;
            if (String.IsNullOrEmpty(this.Email))
            {
             
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe ingresar su email",
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
            var connection = await this.apiServicios.ValidacionInternet();
            if (!connection.respExitosa)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", connection.mensaje, "Aceptar");
                return;
            }
            Usuario usua = await apiServicios.listaUsu(this.Email);


            if (usua != null)
            {
                this.IsRunning = false;
                this.isEnabled = true;


                var url = "https://api201910.azurewebsites.net/".ToString();

                await apiServicios.RecuperarPass(Email, url, "api/RetornoClave", "/enviarClave");
                await Application.Current.MainPage.DisplayAlert(
                        "Mensaje",
                        "Se ha enviado un email a su correo con su contraseña",
                        "Aceptar");
                VistaPrincipal.GetInstancia().Login = new LoginVModelo();
                await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());

            }
            else
            {
                if (usua == null)
                {
                    this.IsRunning = false;
                    this.isEnabled = true;

                    await Application.Current.MainPage.DisplayAlert(
                "Email Erroneo",
                "Este email no está registrado.",
                "Aceptar");
                }

            }

           
        }
    }
}
