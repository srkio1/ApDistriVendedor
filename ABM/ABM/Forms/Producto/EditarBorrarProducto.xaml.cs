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
	public partial class EditarBorrarProducto : ContentPage
	{
		public EditarBorrarProducto (int id_producto, string nombre, string nombre_tipo_producto, string nombre_sub_producto, decimal stock,
            decimal stock_valorado, decimal promedio, decimal precio_venta, decimal producto_alerta)
		{
			InitializeComponent ();

            idProdEntry.Text = id_producto.ToString();
            nombreProdEntry.Text = nombre;
            idTProdEntry.Text = nombre_tipo_producto;
            idSProdEntry.Text = nombre_sub_producto.ToString();
            stockEntry.Text = stock.ToString();
            stockValoradoEntry.Text = stock_valorado.ToString();
            promedioEntry.Text = promedio.ToString();
            precioventaEntry.Text = precio_venta.ToString();
            alertaEntry.Text = producto_alerta.ToString();

        }
        
        private async void BtnEditarProd_Clicked(object sender, EventArgs e)
        {
            Producto producto = new Producto()
            {
                id_producto = Convert.ToInt32(idProdEntry.Text),
                nombre = nombreProdEntry.Text,
                nombre_tipo_producto = idTProdEntry.Text,
                nombre_sub_producto = idSProdEntry.Text,
                stock = Convert.ToDecimal(stockEntry.Text),
                stock_valorado = Convert.ToDecimal(stockValoradoEntry.Text),
                promedio = Convert.ToDecimal(promedioEntry.Text),
                precio_venta = Convert.ToDecimal(precioventaEntry.Text),
                producto_alerta = Convert.ToDecimal(alertaEntry.Text)
            };

            var json = JsonConvert.SerializeObject(producto);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var result = await client.PostAsync("http://dmrbolivia.online/api/productos/editarProducto2.php", content);

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

        private async void BtnBorrarProd_Clicked(object sender, EventArgs e)
        {
            Producto producto = new Producto()
            {
                id_producto = Convert.ToInt32(idProdEntry.Text),
                nombre = nombreProdEntry.Text,
                nombre_tipo_producto = idTProdEntry.Text,
                nombre_sub_producto = idSProdEntry.Text,
                stock = Convert.ToDecimal(stockEntry.Text),
                stock_valorado = Convert.ToDecimal(stockValoradoEntry.Text),
                promedio = Convert.ToDecimal(promedioEntry.Text),
                precio_venta = Convert.ToDecimal(precioventaEntry.Text),
                producto_alerta = Convert.ToDecimal(alertaEntry.Text)
            };

            var json = JsonConvert.SerializeObject(producto);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var result = await client.PostAsync("http://dmrbolivia.online/api/productos/borrarProducto.php", content);

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