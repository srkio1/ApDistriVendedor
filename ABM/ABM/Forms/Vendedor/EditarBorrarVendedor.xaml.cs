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

namespace ABM.Forms.Vendedor
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditarBorrarVendedor : ContentPage
	{
        private string nombre_vendedor;
		public EditarBorrarVendedor (int id, string nombre, int telefono, string direccion, string numero_cuenta, string cedula_identidad)
		{
			InitializeComponent ();
            nombre_vendedor = nombre;
            idEntry.Text = id.ToString();
            nombreEntry.Text = nombre;
            telefonoEntry.Text = telefono.ToString();
            direccionEntry.Text = direccion;
            numero_cuentaEntry.Text = numero_cuenta;
            cedulaEntry.Text = cedula_identidad;
        }

        private async void BtnEditarVendedor_Clicked(object sender, EventArgs e)
        {
            try
            {
                Vendedores vendedores = new Vendedores()
                {
                    id = Convert.ToInt32(idEntry.Text),
                    nombre = nombreEntry.Text,
                    telefono = Convert.ToInt32(telefonoEntry.Text),
                    direccion = direccionEntry.Text,
                    numero_cuenta = numero_cuentaEntry.Text,
                    cedula_identidad = cedulaEntry.Text
                };

                var json = JsonConvert.SerializeObject(vendedores);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();

                var result = await client.PostAsync("http://dmrbolivia.online/api/vendedores/editarVendedor.php", content);

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
            //hola catch
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

        private async void BtnBorrarVendedor_Clicked(object sender, EventArgs e)
        {
            Vendedores vendedores = new Vendedores()
            {
                id = Convert.ToInt32(idEntry.Text),
                nombre = nombreEntry.Text,
                telefono = Convert.ToInt32(telefonoEntry.Text),
                direccion = direccionEntry.Text,
                numero_cuenta = numero_cuentaEntry.Text,
                cedula_identidad = cedulaEntry.Text
            };

            var json = JsonConvert.SerializeObject(vendedores);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var result = await client.PostAsync("http://dmrbolivia.online/api/vendedores/borrarVendedor.php", content);

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

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HistorialVendedor(nombre_vendedor));
        }
    }
}