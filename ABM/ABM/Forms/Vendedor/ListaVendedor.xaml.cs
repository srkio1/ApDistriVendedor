using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABM.Datos;

using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ABM.Forms.Vendedor
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaVendedor : ContentPage
	{
		public ListaVendedor ()
		{
			InitializeComponent ();
            
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarVendedor());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/vendedores/listaVendedores.php");
            var vendedores = JsonConvert.DeserializeObject<List<Vendedores>>(response);

            listaVendedor.ItemsSource = vendedores;
        }

        private async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            var detalles = e.Item as Vendedores;
            await Navigation.PushAsync(new EditarBorrarVendedor(detalles.id, detalles.nombre,
                detalles.telefono, detalles.direccion, detalles.numero_cuenta, detalles.cedula_identidad));
        }

    }
}