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

namespace ABM.Forms.Egresos
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaCostoFijo : ContentPage
	{
        public ListaCostoFijo()
        {
            InitializeComponent();

            
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarCostoFijo());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/egresos/listaCostoFijo.php");
            var costos_fijos = JsonConvert.DeserializeObject<List<Costo_fijo>>(response);

            listCostoFijo.ItemsSource = costos_fijos;
        }

        private async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            var detalles = e.Item as Costo_fijo;
            await Navigation.PushAsync(new EditarBorrarCostoFijo(detalles.id_costo_fijo, detalles.nombre, detalles.monto, detalles.fecha_year));
        }
    }
}