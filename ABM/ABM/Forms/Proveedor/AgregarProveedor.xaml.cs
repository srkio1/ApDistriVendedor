using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using ABM.Datos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ABM.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarProveedor : ContentPage
    {
        public AgregarProveedor()
        {
            InitializeComponent();
        }

       
        private async void BtnGuardarPro_Clicked(object sender, EventArgs e)
        {
            if (nombrePEntry.Text != null)
            {
                if (direccionPEntry.Text != null)
                {
                    if (contactoPEntry.Text != null)
                    {
                        try
                        {
                            Datos.Proveedor proveedor = new Datos.Proveedor()
                            {
                                nombre = nombrePEntry.Text,
                                direccion = direccionPEntry.Text,
                                contacto = contactoPEntry.Text,
                            };

                            var json = JsonConvert.SerializeObject(proveedor);

                            var content = new StringContent(json, Encoding.UTF8, "application/json");

                            HttpClient client = new HttpClient();

                            var result = await client.PostAsync("http://dmrbolivia.online/api/proveedores/agregarProveedor.php", content);

                            if (result.StatusCode == HttpStatusCode.Created)
                            {
                                await DisplayAlert("Hey", "Se agrego correctamente", "Posi mi gresan");
                                await Navigation.PopAsync();
                            }
                            else
                            {
                                await DisplayAlert("Hey", result.StatusCode.ToString(), "Fale Ferga");
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
                    { await DisplayAlert("ERROR", "Es necesario introducir un Contacto", "OK"); }
                }
                else
                { await DisplayAlert("ERROR", "Es necesario introducir una direccion", "OK"); }
            }
            else
            { await DisplayAlert("ERROR", "Es necesario introducir un Nombre", "OK"); }
            
        }
    }
}