using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ABM.Datos;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ABM.Forms.Venta
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditarBorrarVenta : ContentPage
	{
		public EditarBorrarVenta (int ID_VENTA,  DateTime FECHA, int NUMERO_FACTURA ,  string CLIENTE,  string VENDEDOR, string TIPO_VENTA, decimal DESCUENTO, decimal SALDO, decimal TOTAL)
		{
			InitializeComponent ();

            idVentaEntry.Text = Id.ToString();
            numero_facturaVentaEntry.Text = NUMERO_FACTURA.ToString();
            fechaVentaPick.Date = FECHA;
            clienteVentaEntry.Text = CLIENTE;
            vendedorEntry.Text = VENDEDOR;
            tipo_ventaEntry.Text = TIPO_VENTA;
            saldoVentaEntry.Text = SALDO.ToString();
            totalVentaEntry.Text = TOTAL.ToString();

            //using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            //{
            //    conn.CreateTable<DetalleVenta>();
            //    var querydv = conn.Query<DetalleVenta>("select * from DetalleVenta where Factura = '" + NUMERO_FACTURA + "'");
            //    int cont = querydv.Count;
            //    foreach ( var item in querydv)
            //    {
            //        Entry entNomProd = new Entry();
            //        Label label1 = new Label();
            //        label1.Text = "Nombre Producto";
            //        stkProd.Children.Add(label1);
            //        entNomProd.Text = item.nombre_producto;
            //        stkProd.Children.Add(entNomProd);
            //        Entry entCant = new Entry();
            //        Label label2 = new Label();
            //        label2.Text = "Cantidad";
            //        stkProd.Children.Add(label2);
            //        entCant.Text = item.cantidad.ToString();
            //        stkProd.Children.Add(entCant);
            //        Entry entPrec = new Entry();
            //        Label label3 = new Label();
            //        label3.Text = "Precio";
            //        stkProd.Children.Add(label3);
            //        entPrec.Text = item.precio_producto.ToString();
            //        stkProd.Children.Add(entPrec);
            //    }
            //}
        }

        private async void BtnEditarVenta_Clicked(object sender, EventArgs e)
        {
            Ventas ventas = new Ventas()
            {
                id_venta = Convert.ToInt32(idVentaEntry.Text),
                numero_factura = Convert.ToInt32(numero_facturaVentaEntry.Text),
                fecha = fechaVentaPick.Date,
                cliente = clienteVentaEntry.Text,
                vendedor = vendedorEntry.Text,
                tipo_venta = tipo_ventaEntry.Text,
                saldo = Convert.ToInt32(saldoVentaEntry.Text),
                total = Convert.ToInt32(totalVentaEntry.Text)
            };


            var json = JsonConvert.SerializeObject(ventas);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var result = await client.PostAsync("http://dmrbolivia.online/api/ventas/editarVenta.php", content);

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

        private async void BtnBorrarVenta_Clicked(object sender, EventArgs e)
        {
            Ventas ventas = new Ventas()
            {
                id_venta = Convert.ToInt32(idVentaEntry.Text),
                numero_factura = Convert.ToInt32(numero_facturaVentaEntry.Text),
                fecha = fechaVentaPick.Date,
                cliente = clienteVentaEntry.Text,
                vendedor = vendedorEntry.Text,
                tipo_venta = tipo_ventaEntry.Text,
                saldo = Convert.ToInt32(saldoVentaEntry.Text),
                total = Convert.ToInt32(totalVentaEntry.Text)
            };

            var json = JsonConvert.SerializeObject(ventas);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var result = await client.PostAsync("http://dmrbolivia.online/api/ventas/borrarVenta.php", content);

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