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

namespace ABM.Forms.Cliente
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaCliente : ContentPage
	{
		public ListaCliente ()
		{
			InitializeComponent ();
		}
      

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarCliente());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/clientes/listaCliente.php");
            var usuarios = JsonConvert.DeserializeObject<List<Datos.Cliente>>(response);

            listaCliente.ItemsSource = usuarios;
                       
        }

        private async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            var detalles = e.Item as Datos.Cliente;
            await Navigation.PushAsync(new EditarBorrarCliente(detalles.id_cliente, detalles.nombre, detalles.ubicacion_latitud, detalles.ubicacion_longitud, detalles.telefono, detalles.nit));
        }
        private void ToolbarItemMap_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MapaClientes());
        }
    }
}