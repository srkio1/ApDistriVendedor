using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using ABM.Datos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ABM.Forms.Compra
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaCompra : ContentPage
	{
		public ListaCompra ()
		{
			InitializeComponent ();
		}
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarCompra());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/compras/listaCompra.php");
            var compras = JsonConvert.DeserializeObject<List<Datos.Compras>>(response);

            listaComp.ItemsSource = compras;
        }
        private async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            var detalles = e.Item as Compras;
            await Navigation.PushAsync(new MostrarCompra(detalles.id_compra,
                                                         detalles.saldo, detalles.total, detalles.fecha_compra, detalles.hora, 
                                                         detalles.numero_factura,detalles.proveedor));
        }
    }
}