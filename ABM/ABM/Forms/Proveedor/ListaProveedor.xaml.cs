using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ABM.Forms.Proveedor
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaProveedor : ContentPage
	{
		public ListaProveedor ()
		{
			InitializeComponent ();
		}

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarProveedor());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/proveedores/listaProveedor.php");
            var productos = JsonConvert.DeserializeObject<List<Datos.Proveedor>>(response);

            listaProv.ItemsSource = productos;
        
        }
        private async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            var detalles = e.Item as Datos.Proveedor;
            await Navigation.PushAsync(new EditarBorrarProveedor(detalles.id_proveedor, detalles.nombre, detalles.direccion,detalles.contacto, detalles.telefono));
        }

    }
}