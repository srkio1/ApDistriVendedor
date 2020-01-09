using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ABM.Forms.Agenda
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditarAgenda : ContentPage
	{
        public EditarAgenda(int id_agenda, string titulo, DateTime fecha, string hora, string descripcion, string estado)
        {
            InitializeComponent();
            entryAgenda.Text = id_agenda.ToString();
            Entrytitulo.Text = titulo;
            Entryfecha.Date = fecha;
            Entryhora.Text = hora;
            Entrydescripcion.Text = descripcion;
            estadoEntry.SelectedItem = estado;

        }
        private string estadopick;

        private async void Editarr_Clicked(object sender, EventArgs e)
        {
            Datos.Agenda agenda = new Datos.Agenda()
            {
                id_agenda = Convert.ToInt32(entryAgenda.Text),
                titulo = Entrytitulo.Text,
                fecha = Entryfecha.Date,
                hora = Convert.ToString(Entryhora.Text),
                descripcion = Entrydescripcion.Text,
                estado = Convert.ToString(estadoEntry.SelectedItem)
            };

            var json = JsonConvert.SerializeObject(agenda);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var result = await client.PostAsync("http://dmrbolivia.online/api/agenda/editarAgenda.php", content);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                await DisplayAlert("OK", "Se edito correctamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", result.StatusCode.ToString(), "OK");
                await Navigation.PopAsync();
            }

        }

        private void EstadoEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                estadopick = picker.Items[selectedIndex];

            }
        }
    }
}