using ABM.Datos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ABM.Forms
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IniciarSesion : ContentPage
	{
		public IniciarSesion ()
		{
			InitializeComponent ();
		}
        private string NombreVendedor;
        private async void BtnIngresar_Clicked(object sender, EventArgs e)
        {
            indicator_Cargando.IsRunning = true;
            indicator_Cargando.IsVisible = true;
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("http://dmrbolivia.online/api/vendedores/listaVendedores.php");
                var vendedores = JsonConvert.DeserializeObject<List<Vendedores>>(response);

                foreach (var item in vendedores)
                {
                    if (txtUsuario.Text == item.usuario)
                    {
                        if (txtPassword.Text == item.password)
                        {
                            NombreVendedor = item.nombre;
                            await Navigation.PushModalAsync(new HamburgerMenu.HamburgerMenu(NombreVendedor));
                            indicator_Cargando.IsEnabled = false;
                            indicator_Cargando.IsVisible = false;
                        }
                        else
                        {
                            await DisplayAlert("ERROR", "Contrasena incorrecta, porfavor intentelo de nuevo", "OK");
                            indicator_Cargando.IsEnabled = false;
                            indicator_Cargando.IsVisible = false;
                        }
                    }
                    else
                    {
                        await DisplayAlert("ERROR", "Usuario incorrecto, porfavor intentelo de nuevo", "OK");
                        indicator_Cargando.IsEnabled = false;
                        indicator_Cargando.IsVisible = false;
                    }
                }
            }
            catch(Exception err)
            {
                await DisplayAlert("ERROR", "Algo salio mal, intentalo nuevamente", "OK");
                indicator_Cargando.IsEnabled = false;
                indicator_Cargando.IsVisible = false;
            }
        }
    }
}