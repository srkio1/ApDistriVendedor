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
    public partial class AgregarCostoFijo : ContentPage
    {
        public AgregarCostoFijo()
        {
            InitializeComponent();
        }

        private async void BtnAgregarCostoFijo_Clicked(object sender, EventArgs e)
        {
            if (nombreEntry.Text != null)
            {
                if (montoEntry.Text != null)
                {
                    if (fechaMesEntry.Text != null)
                    {

                        try
                        {
                            Costo_fijo costo_Fijo = new Costo_fijo()
                            {

                                nombre = nombreEntry.Text,
                                monto = Convert.ToDecimal(montoEntry.Text),
                                fecha_year = fechaMesEntry.Text
                            };

                            var json = JsonConvert.SerializeObject(costo_Fijo);

                            var content = new StringContent(json, Encoding.UTF8, "application/json");

                            HttpClient client = new HttpClient();

                            var result = await client.PostAsync("http://dmrbolivia.online/api/egresos/agregarCostoFijo.php", content);

                            if (result.StatusCode == HttpStatusCode.Created)
                            {
                                await DisplayAlert("Agregado", "Se agrego correctamente", "OK");
                                await Navigation.PopAsync();
                            }
                            else
                            {
                                await DisplayAlert("Error", result.StatusCode.ToString(), "Ocurrio un error");
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
                    else { await DisplayAlert("ERROR", "Es necesario introducir una Fecha", "OK"); }
                }
                else
                { await DisplayAlert("ERROR", "Es necesario introducir un Monto", "OK"); }
            }
            else
            { await DisplayAlert("ERROR", "Es necesario introducir un Nombre", "OK"); }
        }
    }
}