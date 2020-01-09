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
	public partial class EditarBorrarCliente : ContentPage
	{
        private string Nombre_cliente;
        private int IDCLIENTE;
		public EditarBorrarCliente (int id_cliente, string nombre, string ubicacion_latitud, string ubicacion_longitud,  int telefono, int nit)
		{
			InitializeComponent ();
            IDCLIENTE = id_cliente;
            Nombre_cliente = nombre;
            idClienteEntry.Text = id_cliente.ToString();
            nombreClienteEntry.Text = nombre;
            ubicacionLatitudEntry.Text = ubicacion_latitud;
            ubicacionLongitudEntry.Text = ubicacion_longitud;
            telefonoClienteEntry.Text = telefono.ToString();
            nitClienteEntry.Text = nit.ToString();
		}

        private async void BtnEditarCliente_Clicked(object sender, EventArgs e)
        {
            if (IDCLIENTE != 0)
            {
                if (nombreClienteEntry.Text.Length > 0)
                {
                    if (telefonoClienteEntry.Text.Length > 7)
                    {
                        if (nitClienteEntry.Text.Length > 6)
                        {


                            try
                            {
                                Datos.Cliente cliente = new Datos.Cliente()
                                {
                                    id_cliente = Convert.ToInt32(idClienteEntry.Text),
                                    nombre = nombreClienteEntry.Text,
                                    ubicacion_latitud = ubicacionLatitudEntry.Text,
                                    ubicacion_longitud = ubicacionLongitudEntry.Text,
                                    telefono = Convert.ToInt32(telefonoClienteEntry.Text),
                                    nit = Convert.ToInt32(nitClienteEntry.Text)
                                };

                                var json = JsonConvert.SerializeObject(cliente);

                                var content = new StringContent(json, Encoding.UTF8, "application/json");

                                HttpClient client = new HttpClient();

                                var result = await client.PostAsync("http://dmrbolivia.online/api/clientes/editarCliente.php", content);

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
                        else
                        { await DisplayAlert("ERROR", "El campo de Telefono requiere minimo 7 digitos", "OK"); }
                    }
                    else
                    { await DisplayAlert("ERROR", "El campo de Telefono requiere minimo 8 digitos", "OK"); }
                }
                else
                {
                    await DisplayAlert("ERROR", "El campo de Nombre es necesario", "OK");
                }
            }

            else
            {
                await DisplayAlert("ERROR", "Algo salio mal, intentelo de nuevo", "OK");
                await Navigation.PopAsync();
            }
        }

        private async void BtnBorrarCliente_Clicked(object sender, EventArgs e)
        {
            
            Datos.Cliente cliente = new Datos.Cliente()
            {
                id_cliente = Convert.ToInt32(idClienteEntry.Text),
                nombre = nombreClienteEntry.Text,
                ubicacion_latitud = ubicacionLatitudEntry.Text,
                ubicacion_longitud = ubicacionLongitudEntry.Text,
                telefono = Convert.ToInt32(telefonoClienteEntry.Text),
                nit = Convert.ToInt32(nitClienteEntry.Text)
            };

            var json = JsonConvert.SerializeObject(cliente);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var result = await client.PostAsync("http://dmrbolivia.online/api/clientes/borrarCliente.php", content);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                await DisplayAlert("Borrar", "Se elimino correctamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", result.StatusCode.ToString(), "OK");
                await Navigation.PopAsync();
            }            
        }

        private async void BtnVerUbicacion_Clicked(object sender, EventArgs e)
        {
            var location = new Location(Convert.ToDouble(ubicacionLatitudEntry.Text), Convert.ToDouble(ubicacionLongitudEntry.Text));
            var options = new MapLaunchOptions { Name = nombreClienteEntry.Text };
            await Map.OpenAsync(location, options);
            
        }

        private async void BtnObtenerUbicacion_Clicked(object sender, EventArgs e)
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

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HistorialCliente(Nombre_cliente));
        }
    }
}