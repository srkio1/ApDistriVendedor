using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ABM.Forms.Agenda
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListadoHistorialAgenda : ContentPage
	{
		public ListadoHistorialAgenda ()
		{
			InitializeComponent ();
            
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/agenda/listaAgenda.php");
            var agenda = JsonConvert.DeserializeObject<List<Datos.Agenda>>(response);
            listAgendahistorial.ItemsSource = agenda;

        }

        private async void ListAgendahistorial_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var detalles = e.Item as Datos.Agenda;
            await Navigation.PushAsync(new EditarAgenda(detalles.id_agenda, detalles.titulo, detalles.fecha, detalles.hora, detalles.descripcion, detalles.estado));
        }
    }
}