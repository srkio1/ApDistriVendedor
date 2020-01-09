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
    public partial class Listaagenda : ContentPage
    {
        public Listaagenda()
        {
            InitializeComponent();
           
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/agenda/listaAgendaActivo.php");
            var agenda = JsonConvert.DeserializeObject<List<Datos.Agenda>>(response);
            listAgenda.ItemsSource = agenda;

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarAgenda());
        }

        private async void ListAgenda_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var detalles = e.Item as Datos.Agenda;
            await Navigation.PushAsync(new EditarAgenda(detalles.id_agenda, detalles.titulo, detalles.fecha, detalles.hora, detalles.descripcion, detalles.estado));
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListadoHistorialAgenda());
        }
    }

    }
