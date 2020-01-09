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

namespace ABM.Forms.Venta
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaVenta : ContentPage
	{
        private string Nombrevendedor;
        public ListaVenta(string nombreVendedor)
		{
			InitializeComponent ();
            Nombrevendedor = nombreVendedor;
            toolbar.Text = "AGREGAR";
		}

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarVenta(Nombrevendedor));
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/ventas/listaVenta.php");
            var ventas = JsonConvert.DeserializeObject<List<Datos.Ventas>>(response);

            listaVenta.ItemsSource = ventas;
        }

        private async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            var detalles = e.Item as Ventas;
            await Navigation.PushAsync(new MostrarVenta(detalles.id_venta, detalles.fecha, detalles.hora, detalles.numero_factura, detalles.cliente,  
                                                        detalles.vendedor,  detalles.tipo_venta, detalles.descuento, detalles.saldo, detalles.total));
        }
    }
}