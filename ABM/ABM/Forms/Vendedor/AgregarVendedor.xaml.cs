using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABM.Datos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace ABM.Forms.Vendedor
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AgregarVendedor : ContentPage
	{
		public AgregarVendedor ()
		{
			InitializeComponent ();
		}

        private async void BtnGuardarVendedor_Clicked(object sender, EventArgs e)
        {
            if (nombreVendedorEntry.Text != null)
            {
                if (telefonoVendedorEntry.Text != null)
                {
                    if (direccionVendedorEntry.Text != null)
                    {
                        if (cedula_identidadVendedorEntry.Text != null)
                        {
                            try
                            {
                                Vendedores vendedores = new Vendedores()
                                {
                                    nombre = nombreVendedorEntry.Text,
                                    telefono = Convert.ToInt32(telefonoVendedorEntry.Text),
                                    direccion = direccionVendedorEntry.Text,
                                    numero_cuenta = numero_cuentaVendedorEntry.Text,
                                    cedula_identidad = cedula_identidadVendedorEntry.Text
                                };

                                var json = JsonConvert.SerializeObject(vendedores);

                                var content = new StringContent(json, Encoding.UTF8, "application/json");

                                HttpClient client = new HttpClient();

                                var result = await client.PostAsync("http://dmrbolivia.online/api/vendedores/agregarVendedor.php", content);

                                if (result.StatusCode == HttpStatusCode.OK)
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
                        { await DisplayAlert("ERROR", "Es necesario introducir una Cedula de identidad", "OK"); }
                    }
                    else
                    {
                        await DisplayAlert("ERROR", "Es necesario introducir una Direccion", "OK");
                    }
                }
                else
                { await DisplayAlert("ERROR", "Es necesario introducir un Telefono", "OK"); }
            }
            else
            { await DisplayAlert("ERROR", "Es necesario introducir un Nombre", "OK"); }
        }
    }
}