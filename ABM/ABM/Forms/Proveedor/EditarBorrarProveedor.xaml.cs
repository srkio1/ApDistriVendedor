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

namespace ABM.Forms.Proveedor
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditarBorrarProveedor : ContentPage
	{
		public EditarBorrarProveedor (int id_proveedor, string nombre, string direccion, string contacto, int telefono)
		{
			InitializeComponent ();

            idProvEntry.Text = id_proveedor.ToString();
            nombrePEntry.Text = nombre.ToString();
            direccionPEntry.Text = direccion.ToString();
            contactoPEntry.Text = contacto.ToString();
            telefonoPEntry.Text = telefono.ToString();
		}

        private async void BtneditarProveedor_Clicked(object sender, EventArgs e)
        {
            if (nombrePEntry.Text != null)
            {
                if (direccionPEntry.Text != null)
                {
                    if (contactoPEntry.Text != null)
                    {
                        if (telefonoPEntry.Text != null)
                        {
                            try
                            {
                                Datos.Proveedor proveedor = new Datos.Proveedor()
                                {
                                    id_proveedor = Convert.ToInt32(idProvEntry.Text),
                                    nombre = nombrePEntry.Text,
                                    direccion = direccionPEntry.Text,
                                    contacto = contactoPEntry.Text,
                                    telefono = Convert.ToInt32(telefonoPEntry.Text),
                                };

                                var json = JsonConvert.SerializeObject(proveedor);

                                var content = new StringContent(json, Encoding.UTF8, "application/json");

                                HttpClient client = new HttpClient();

                                var result = await client.PostAsync("http://dmrbolivia.online/api/proveedores/editarProveedor.php", content);

                                if (result.StatusCode == HttpStatusCode.OK)
                                {
                                    await DisplayAlert("Hey", "Se agrego correctamente", "Puede Continuar");
                                    await Navigation.PopAsync();
                                }
                                else
                                {
                                    await DisplayAlert("Hey algo salio mal", result.StatusCode.ToString(), "Fale Ferga");
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
                        { await DisplayAlert("ERROR", "Es necesario introducir un Telefono", "OK"); }
                    }
                    else
                    { await DisplayAlert("ERROR", "Es necesario introducir un Contacto", "OK"); }
                }
                else
                { await DisplayAlert("ERROR", "Es necesario introducir una Direccion", "OK"); }
            }
            else
            { await DisplayAlert("ERROR", "Es necesario introducir un Nombre", "OK"); }

        }

        private async void BtnborrarProveedor_Clicked(object sender, EventArgs e)
        {
            Datos.Proveedor proveedor = new Datos.Proveedor()
            {
                id_proveedor = Convert.ToInt32(idProvEntry.Text),
                nombre = nombrePEntry.Text,
                direccion = direccionPEntry.Text,
                contacto = contactoPEntry.Text,
                telefono = Convert.ToInt32(telefonoPEntry.Text),
            };
            var json = JsonConvert.SerializeObject(proveedor);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var result = await client.PostAsync("http://dmrbolivia.online/api/proveedores/borrarProveedor.php", content);

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
    }
}