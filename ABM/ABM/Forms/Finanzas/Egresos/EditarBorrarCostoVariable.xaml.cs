using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using ABM.Datos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ABM.Forms.Egresos
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditarBorrarCostoVariable : ContentPage
	{
        public EditarBorrarCostoVariable(int id_cv, string nombre, DateTime fecha_dia, string fecha_mes, string descripcion, decimal monto)
        {
            InitializeComponent();

            idCostoVEntry.Text = id_cv.ToString();
            nombreEntry.Text = nombre;
            fechadiaEntry.Text = fecha_dia.ToString();
            fechamesEntry.Text = fecha_mes;
            descripcionEntry.Text = descripcion;
            montoEntry.Text = monto.ToString();

        }

        private async void BtnEditarCV_Clicked(object sender, EventArgs e)
        {
            if (nombreEntry.Text != null)
            {
                if (fechadiaEntry.Text != null)
                {
                    if (fechamesEntry.Text != null)
                    {
                        if (montoEntry.Text != null)
                        {
                            try
                            {
                                Costo_variable costo_Variable = new Costo_variable()
                                {
                                    id_cv = Convert.ToInt32(idCostoVEntry.Text),
                                    nombre = nombreEntry.Text,
                                    fecha_dia = Convert.ToDateTime(fechadiaEntry.Text),
                                    fecha_mes = fechamesEntry.Text,
                                    descripcion = descripcionEntry.Text,
                                    monto = Convert.ToDecimal(montoEntry.Text)


                                };

                                var json = JsonConvert.SerializeObject(costo_Variable);

                                var content = new StringContent(json, Encoding.UTF8, "application/json");

                                HttpClient client = new HttpClient();

                                var result = await client.PostAsync("http://dmrbolivia.online/api/egresos/editarCostoVariable.php", content);

                                if (result.StatusCode == HttpStatusCode.OK)
                                {
                                    await DisplayAlert("Hey", "Se edito correctamente", "OK");
                                    Navigation.PopAsync();
                                }
                                else
                                {
                                    await DisplayAlert("Error", result.StatusCode.ToString(), "Ocurrio un error");
                                    Navigation.PopAsync();
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
                        else { await DisplayAlert("ERROR", "Es necesario introducir un Monto", "OK"); }
                    }
                    else
                    { await DisplayAlert("ERROR", "Es necesario introducir un Mes", "OK"); }
                }
                else
                { await DisplayAlert("ERROR", "Es necesario introducir un Dia", "OK"); }
            }
            else
            { await DisplayAlert("ERROR", "Es necesario introducir un Nombre", "OK"); }
        }


        private async void BtnBorrarCV_Clicked(object sender, EventArgs e)
        {
            Costo_variable costo_Variable = new Costo_variable()
            {
                id_cv = Convert.ToInt32(idCostoVEntry.Text),
                nombre = nombreEntry.Text,                
                fecha_dia = Convert.ToDateTime(fechadiaEntry.Text),
                fecha_mes = fechamesEntry.Text,
                descripcion = descripcionEntry.Text,
                monto = Convert.ToDecimal(montoEntry.Text)
            };

            var json = JsonConvert.SerializeObject(costo_Variable);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();

            var result = await client.PostAsync("http://dmrbolivia.online/api/egresos/borrarCostoVariable.php", content);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                await DisplayAlert("Eliminar", "Se elimino correctamente", "OK");
                Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", result.StatusCode.ToString(), "No se pudo Eliminar");
                Navigation.PopAsync();
            }
        }
    }
}