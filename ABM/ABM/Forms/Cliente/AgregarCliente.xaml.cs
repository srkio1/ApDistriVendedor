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
using Xamarin.Essentials;

namespace ABM.Forms.Cliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarCliente : ContentPage
    {
       
        
        public AgregarCliente()
        {
            InitializeComponent();
        }

        private async void BtnGuardarCliente_Clicked(object sender, EventArgs e)
        {
            int telefonovalidar = 0;
            int nitvalidar = 0;
            nitvalidar = Convert.ToInt32(nitEntry);
            telefonovalidar =Convert.ToInt32( telefonoEntry.Text);
            try
            {
                if (nombreEntry.Text != null)
                {
                    if (telefonovalidar != 0)
                    {
                        if (nitvalidar != 0)
                        {

                            Datos.Cliente cliente = new Datos.Cliente()
                            {
                                nombre = nombreEntry.Text,
                                ubicacion_latitud = ubicacionLatitudEntry.Text,
                                ubicacion_longitud = ubicacionLongitudEntry.Text,
                                telefono = Convert.ToInt32(telefonoEntry.Text),
                                nit = Convert.ToInt32(nitEntry.Text)
                            };

                            var json = JsonConvert.SerializeObject(cliente);

                            var content = new StringContent(json, Encoding.UTF8, "application/json");

                            HttpClient client = new HttpClient();

                            var result = await client.PostAsync("http://dmrbolivia.online/api/clientes/agregarCliente.php", content);

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
                        else
                        { await DisplayAlert("ERROR", "Es necesario introducir un Nit", "OK"); }
                    }

                    else
                    {
                        await DisplayAlert("ERROR", "Es necesario introducir un Telefono", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("ERROR", "Es necesario introducir un Nombre", "OK");
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

        async void BtnUbicacion_Clicked(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    ubicacionLatitudEntry.Text = location.Latitude.ToString();
                    ubicacionLongitudEntry.Text = location.Longitude.ToString();
                    ubconfirmacionEntry.Text = "Ubicacion Guardada";
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Faild", fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Faild", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Faild", ex.Message, "OK");
            }
        }
    }
}