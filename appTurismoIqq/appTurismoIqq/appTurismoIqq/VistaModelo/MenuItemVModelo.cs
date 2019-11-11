using appTurismoIqq.Helpers;
using appTurismoIqq.Modelo;
using appTurismoIqq.Vistas;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace appTurismoIqq.VistaModelo
{
    public class MenuItemVModelo:Modelo.Menu
    {
        public ICommand SelectMenuCommand
        {
            get
            {
                return new RelayCommand(SelectMenu);

            }
        }

        private async void SelectMenu()
        {
            App.Master.IsPresented = false;
            var vistaPrincipal = VistaPrincipal.GetInstancia();
            switch (this.Pagina)
            {
                case "DescuentoPage":
                    await App.Navigator.PushAsync(new DescuentoPage());
                    break;
                /*case "SetupPage":
                    await App.Navigator.PushAsync(new SetupPage());
                    break;*/
               /* case "ProfilePage":
                    vistaPrincipal.Profile = new ProfileViewModel();
                    await App.Navigator.PushAsync(new ProfilePage());
                    break;*/
                default:
                    //Settings.User = string.Empty;
                    Settings.IsRemembered = false;
                   // Settings.Token = string.Empty;
                   // Settings.UserEmail = string.Empty;
                   // Settings.UserPassword = string.Empty;

                    VistaPrincipal.GetInstancia().Login = new LoginVModelo();
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    break;
            }
        }
    }
}
