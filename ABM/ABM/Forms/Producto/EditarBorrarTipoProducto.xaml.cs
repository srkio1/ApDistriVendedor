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
	public partial class EditarBorrarTipoProducto : ContentPage
	{
		public EditarBorrarTipoProducto ( int id_tipoproducto, string nombre_tipo_producto)
		{
			InitializeComponent ();
            idproductosentry.Text = id_tipoproducto.ToString(); 
            nombreTpEntry.Text = nombre_tipo_producto;
            
		}

        private async void BtnEditarTP_Clicked(object sender, EventArgs e)
        {
            Tipo_producto tipo_Producto = new Tipo_producto()
            {
                id_tipoproducto =Convert.ToInt32( idproductosentry.Text),
                 
                nombre_tipo_producto = nombreTpEntry.Text
            };

            var json = JsonConvert.SerializeObject(tipo_Producto);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var result = await client.PostAsync("http://dmrbolivia.online/api/tipoproductos/editarTipoproducto.php", content);

            if (result.StatusCode == HttpStatusCode.OK)
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

        private async void BtnBorrarTP_Clicked(object sender, EventArgs e)
        {
            Tipo_producto tipo_Producto = new Tipo_producto()
            {
                id_tipoproducto = Convert.ToInt32(idproductosentry.Text),
                nombre_tipo_producto = nombreTpEntry.Text
            };

            var json = JsonConvert.SerializeObject(tipo_Producto);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var result = await client.PostAsync("http://dmrbolivia.online/api/tipoproductos/borrarTipoproducto.php", content);

            if (result.StatusCode == HttpStatusCode.OK)
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