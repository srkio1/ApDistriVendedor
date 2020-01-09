using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using ABM.Datos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ABM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AgregarSubProducto : ContentPage
	{
		public AgregarSubProducto ()
		{
			InitializeComponent ();
		}

        private async void BtnGuardarSP_Clicked(object sender, EventArgs e)
        {
            Sub_producto sub_Producto = new Sub_producto()
            {
                nombre_sub_producto = nombreSpEntry.Text
            };

            var json = JsonConvert.SerializeObject(sub_Producto);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var result = await client.PostAsync("http://dmrbolivia.online/api/subproductos/agregarSubproducto.php", content);

            if (result.StatusCode == HttpStatusCode.Created)
            {
                await DisplayAlert("Hey", "Se agrego correctamente", "Posi mi gresan");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Hey", result.StatusCode.ToString(), "Fale Ferga");
                await Navigation.PopAsync();
            }            
        }
    }
}