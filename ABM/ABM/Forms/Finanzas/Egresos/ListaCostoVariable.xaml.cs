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
	public partial class ListaCostoVariable : ContentPage
	{
        public ListaCostoVariable()
        {
            InitializeComponent();

            GetCosto_variable();

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarCostoVariable());
        }

        private async void GetCosto_variable()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/egresos/listaCostoVariable.php");
            var costos_variables = JsonConvert.DeserializeObject<List<Costo_variable>>(response);

            listCostoVariable.ItemsSource = costos_variables;
        }

        private async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            var detalles = e.Item as Costo_variable;
            await Navigation.PushAsync(new EditarBorrarCostoVariable(detalles.id_cv, detalles.nombre, detalles.fecha_dia, detalles.fecha_mes, detalles.descripcion, detalles.monto));
        }
    }
}