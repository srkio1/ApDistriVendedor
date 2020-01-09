using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABM.Datos;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ABM.Forms.Egresos
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AgregarCostoVariable : ContentPage
	{
        public AgregarCostoVariable()
        {
            InitializeComponent();
        }
        string mesPick;


        private void Tipomes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                mesPick = picker.Items[selectedIndex];
            }
        }


        private async void BtnGuardarCostoVariable_Clicked(object sender, EventArgs e)
        {
            if (nombreEntry.Text != null)
            {
                if (fechadiaEntry.Text != null)
                {
                    if (mesPick != null)
                    {
                        if (montoEntry.Text != null)
                        {
                            try
                            {
                                Costo_variable costo_Variable = new Costo_variable()
                                {

                                    nombre = nombreEntry.Text,
                                    fecha_dia = Convert.ToDateTime(fechadiaEntry.Text),
                                    fecha_mes = mesPick,
                                    descripcion = descripcionEntry.Text,
                                    monto = Convert.ToDecimal(montoEntry.Text),

                                };

                                var json = JsonConvert.SerializeObject(costo_Variable);

                                var content = new StringContent(json, Encoding.UTF8, "application/json");

                                HttpClient client = new HttpClient();

                                var result = await client.PostAsync("http://dmrbolivia.online/api/egresos/agregarCostoVariable.php", content);

                                if (result.StatusCode == HttpStatusCode.Created)
                                {
                                    await DisplayAlert("Guardado", "Se guardo correctamente", "OK");
                                    await Navigation.PopAsync();
                                }
                                else
                                {
                                    await DisplayAlert("Error", result.StatusCode.ToString(), "Ocurrio un Error");
                                    await Navigation.PopAsync();
                                }
                            }
                            catch (Exception err)
                            {
                                await DisplayAlert("Error", "Algo salio mal, intentalo de nuevo", "OK");
                                Reporte_Log reportesLogs = new Reporte_Log()
                                {
                                    descripcion = err.ToString(),
                                    fecha = DateTime.Now.ToLocalTime()
                                };
                                var json = JsonConvert.SerializeObject(reportesLogs);
                                var content = new StringContent(json, Encoding.UTF8, "application/json");
                                HttpClient client = new HttpClient();
                                var result = await client.PostAsync("http://dmrbolivia.online/api/agregarReporteLog.php", content);
                            }
                        }
                        else { await DisplayAlert("ERROR", "Es necesario introducir una monto", "OK"); }
                    }
                    else
                    { await DisplayAlert("ERROR", "Es necesario introducir una Mes", "OK"); }
                }
                else
                { await DisplayAlert("ERROR", "Es necesario introducir un dia", "OK"); }
            }
            else
            {
                await DisplayAlert("ERROR", "Es necesario introducir un Nombre", "OK");
            }
        }

        
    }
}