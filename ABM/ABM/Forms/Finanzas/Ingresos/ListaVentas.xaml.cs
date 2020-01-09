using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABM.Datos;
using ABM.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace ABM.Forms.Finanzas.Ingresos
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaVentas : ContentPage
	{
		public ListaVentas ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/ingresos/listaIngreso.php");
            var lista_ventas = JsonConvert.DeserializeObject<List<Ventas>>(response);
                        
            listFinanzas.ItemsSource = lista_ventas;
                       
        }
        private async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            var detalles = e.Item as Ventas;
            await Navigation.PushAsync(new Venta.MostrarVenta(detalles.id_venta, detalles.fecha, detalles.hora, detalles.numero_factura, detalles.cliente, detalles.vendedor, detalles.tipo_venta, detalles.descuento, detalles.saldo, detalles.total));
        }
    }
}