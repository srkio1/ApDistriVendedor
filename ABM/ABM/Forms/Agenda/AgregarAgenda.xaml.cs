using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABM.Datos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
namespace ABM.Forms.Agenda
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarAgenda : ContentPage
    {
        public AgregarAgenda()
        {
            InitializeComponent();
        }
        private async void Rgistrar_Clicked(object sender, EventArgs e)
        {
            Datos.Agenda agenda = new Datos.Agenda()
            {
                titulo = Entrytitulo.Text,
                fecha = Entryfecha.Date,
                hora = Convert.ToString(Entryhora.Time),
                descripcion = Entrydescripcion.Text,
                estado = pickEstado

            };
            var json = JsonConvert.SerializeObject(agenda);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var result = await client.PostAsync("http://dmrbolivia.online/api/agenda/agregarAgenda.php", content);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                await DisplayAlert("OK", "Se agrego correctamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", result.StatusCode.ToString(), "OK");
                await Navigation.PopAsync();
            }
        }
        private string pickEstado;
        private void TipoVentaEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectIndex = picker.SelectedIndex;
            if (selectIndex != -1)
            {
                pickEstado = picker.Items[selectIndex];
            }

        }
    }
}