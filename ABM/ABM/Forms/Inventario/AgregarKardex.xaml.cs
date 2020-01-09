using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABM.Datos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net.Http;

namespace ABM.Forms.Inventario
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AgregarKardex : ContentPage
	{
		public AgregarKardex ()
		{
			InitializeComponent ();
		}


        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Datos.Inventario inventario = new Datos.Inventario()
                {
                    nombre_p = nombreProductoEntry.Text,
                    fecha_inv = fechaInvetarioEntry.Date,
                    numero_factura = Convert.ToInt32(numero_facturaEntry.Text),
                    detalle = detalleEntry.Text,
                    precio_compra = Convert.ToDecimal(precio_compraEntry.Text),
                    unidades = Convert.ToDecimal(unidadesEntry.Text),
                    entrada_fisica = Convert.ToDecimal(entrada_fiscaEntry.Text),
                    salida_fisica = Convert.ToDecimal(saldo_fisicaEntry.Text),
                    saldo_fisica = Convert.ToDecimal(saldo_fisicaEntry.Text),
                    entrada_valorado = Convert.ToDecimal(entrada_valoradoEntry.Text),
                    salida_valorado = Convert.ToDecimal(salida_valoradoEntry.Text),
                    saldo_valorado = Convert.ToDecimal(saldo_valoradoEntry.Text),
                    promedio = Convert.ToDecimal(promedioEntry.Text)
                };

                var json = JsonConvert.SerializeObject(inventario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                var result = await client.PostAsync("http://dmrbolivia.online/api/invetarios/agregarInventario.php", content);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
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
            catch (Exception result) {await DisplayAlert("Error", result.ToString(), "OK"); }
        }
    }
}