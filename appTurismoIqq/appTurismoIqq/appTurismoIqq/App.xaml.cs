using appTurismoIqq.Helpers;
using appTurismoIqq.VistaModelo;
using appTurismoIqq.Vistas;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace appTurismoIqq
{
    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }

        public App()
        {
            InitializeComponent();

            var vPrincipal = VistaPrincipal.GetInstancia();
            if (Settings.IsRemembered)
            {
                vPrincipal.Categorias = new CategoriasVModel();
                this.MainPage = new MasterPage();
            }
            else
            {

                vPrincipal.Login = new LoginVModelo();
                this.MainPage = new NavigationPage(new LoginPage());
            }

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
