using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABM.Datos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;

namespace ABM.Forms.Compra
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditarBorrarCompra : ContentPage
	{
		public EditarBorrarCompra (int ID_COMPRA, decimal SALDO, decimal TOTAL, DateTime FECHA_COMPRA, int NUMERO_FACTURA, string PROVEEDOR)
		{
			InitializeComponent ();

            idCompraEntry.Text = ID_COMPRA.ToString();            
            saldoCompraEntry.Text = SALDO.ToString();
            totalCompraEntry.Text = TOTAL.ToString();
            fechaCompraPick.Date = FECHA_COMPRA;
            numero_facturaCompraEntry.Text = NUMERO_FACTURA.ToString();
            proveedorPEntry.Text = PROVEEDOR.ToString();

		}

        private string pickProveedor;
        private void ProveedorPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectIndex = picker.SelectedIndex;
            if (selectIndex != 1)
            {
                pickProveedor = picker.Items[selectIndex];
            }
        }

        private async void BtnEditarComp_Clicekd(object sender, EventArgs e)
        {
            Compras compra = new Compras()
            {
                id_compra =Convert.ToInt32( idCompraEntry.Text),
                proveedor = proveedorPEntry.Text,
                fecha_compra = fechaCompraPick.Date,
                numero_factura = Convert.ToInt32(numero_facturaCompraEntry.Text),
                saldo = Convert.ToDecimal(saldoCompraEntry.Text),
                total = Convert.ToDecimal(totalCompraEntry.Text)
            };

            var json = JsonConvert.SerializeObject(compra);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var result = await client.PostAsync("http://dmrbolivia.online/api/compras/editarCompra.php", content);

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
        private async void BtnBorrarComp_Clicked(object sender, EventArgs e)
        {
            Compras compra = new Compras()
            {
                id_compra = Convert.ToInt32(idCompraEntry.Text),
                saldo = Convert.ToInt32(saldoCompraEntry.Text),
                fecha_compra = fechaCompraPick.Date,
                numero_factura = Convert.ToInt32(numero_facturaCompraEntry.Text),
                proveedor = proveedorPEntry.Text
            };
            var json = JsonConvert.SerializeObject(compra);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var result = await client.PostAsync("http://dmrbolivia.online/api/compras/borrarCompra.php", content);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                await DisplayAlert("Borrar", "Se Borro correctamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Borrar", result.StatusCode.ToString(), "Ocurrio un Error");
                await Navigation.PopAsync();
            }
        }

	}
}