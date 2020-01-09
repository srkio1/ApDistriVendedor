using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ABM.Forms;
using ABM;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ABM
{
    
    public partial class App : Application
    {
        private string NombreVendedor;
        public static string FilePath;
        //public App()
        //{
        //    InitializeComponent();

        //    MainPage = new NavigationPage(new AgregarTipoProducto());
        //}

        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new MainPage());

            //MainPage = new HamburgerMenu.HamburgerMenu(NombreVendedor);
            MainPage = new NavigationPage(new IniciarSesion());
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
