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
	public partial class EditarBorrarCostoFijo : ContentPage
	{
        public EditarBorrarCostoFijo(int id_costo_fijo, string nombre, decimal monto, string fecha_year)
        {
            InitializeComponent();

            idCostoFijoEntry.Text = id_costo_fijo.ToString();
            nombreEntry.Text = nombre;
            montoEntry.Text = monto.ToString();
            fechaEntry.Text = fecha_year;
        }

        private async void BtnEditarCostoFijo_Clicked(object sender, EventArgs e)
        {
            if (nombreEntry.Text != null)
            {
                if (montoEntry.Text != null)
                {
                    if (fechaEntry.Text != null)
                    {
                        try
                        {
                            Costo_fijo costo_Fijo = new Costo_fijo()
                            {
                                id_costo_fijo = Convert.ToInt32(idCostoFijoEntry.Text),
                                nombre = nombreEntry.Text,
                                monto = Convert.ToDecimal(montoEntry.Text),
                                fecha_year = fechaEntry.Text
                            };

                            var json = JsonConvert.SerializeObject(costo_Fijo);

                            var content = new StringContent(json, Encoding.UTF8, "application/json");

                            HttpClient client = new HttpClient();

                            var result = await client.PostAsync("http://dmrbolivia.online/api/egresos/editarCostoFijo.php", content);

                            if (result.StatusCode == HttpStatusCode.OK)
                            {
                                await DisplayAlert("Editado", "Se edito correctamente", "OK");
                                await Navigation.PopAsync();
                            }
                            else
                            {
                                await DisplayAlert("Error", result.StatusCode.ToString(), "No se Edito");
                                await Navigation.PopAsync();
                            }
                        }
                        catch (Exception err1)
                        {
                            await DisplayAlert("Error", "Algo salio mal, intentalo de nuevo", "OK");
                            Reporte_Log reportesLogs = new Reporte_Log()
                            {
                                descripcion = err1.ToString(),
                                fecha = DateTime.Now.ToLocalTime()
                            };
                            var json = JsonConvert.SerializeObject(reportesLogs);
                            var content = new StringContent(json, Encoding.UTF8, "application/json");
                            HttpClient client = new HttpClient();
                            var result = await client.PostAsync("http://dmrbolivia.online/api/agregarReporteLog.php", content);
                        }
                    }
                    else
                    { await DisplayAlert("ERROR", "Es necesario introducir una Fecha", "OK"); }
                }
                else
                {
                    await DisplayAlert("ERROR", "Es necesario introducir un Monto", "OK");
                }
            }
            else
            { await DisplayAlert("ERROR", "Es necesario introducir un Nombre", "OK"); }
        }

        private async void BtnBorrarCostoFijo_Clicked(object sender, EventArgs e)
        {
            Costo_fijo costo_Fijo = new Costo_fijo()
            {
                id_costo_fijo = Convert.ToInt32(idCostoFijoEntry.Text),
                nombre = nombreEntry.Text,
                monto = Convert.ToDecimal(montoEntry.Text),
                fecha_year = fechaEntry.Text
            };

            var json = JsonConvert.SerializeObject(costo_Fijo);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();

            var result = await client.PostAsync("http://dmrbolivia.online/api/egresos/borrarCostoFijo.php", content);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                await DisplayAlert("Borrar", "Se elimino correctamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", result.StatusCode.ToString(), "No se Elimino");
                await Navigation.PopAsync();
            }
        }
    }
}