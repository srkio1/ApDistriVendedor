using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABM.Datos;
using ABM.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;
using System.Threading;
using System.Net.Http;
using Newtonsoft.Json;

namespace ABM.Forms
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Index : ContentPage
	{
        public Index()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/ventas/listaDetalleVenta.php");
            var detalleVentas = JsonConvert.DeserializeObject<List<DetalleVenta>>(response);

            string fechaActual = DateTime.Today.ToString("dd/MM/yyy");
            txtFechaActual.Text = fechaActual;
            int CantidadVendida = 0;
            int cantidadotro = 0;
            foreach (var item in detalleVentas)
            {
                CantidadVendida = CantidadVendida + item.cantidad;
            }
            txtCajasVendidas2.Text = CantidadVendida.ToString();

            var response1 = await client.GetStringAsync("http://dmrbolivia.online/api/productos/listaProducto.php");
            var prod_check = JsonConvert.DeserializeObject<List<Producto>>(response1);
            foreach (var itemProd in prod_check)
            {
                if (itemProd.producto_alerta >= itemProd.stock)
                {
                    await DisplayAlert("Productos", "Producto " + itemProd.display_text_nombre + " bajo nivel de stock", "OK");
                    txtprueba1.Text = itemProd.producto_alerta.ToString();
                    txtprueba2.Text = itemProd.stock.ToString();

                }
                else
                {

                }

            }

            //Fechas mes anterior
            DateTime fechaMesAnt = DateTime.Today.AddMonths(-1);
            DateTime primerDiaMesAnt = new DateTime(fechaMesAnt.Year, fechaMesAnt.Month, 1);
            DateTime ultimoDiaMesAnt = primerDiaMesAnt.AddMonths(1).AddDays(-1);
            //Fechas mes actual
            DateTime fechaMesAct = DateTime.Today;
            DateTime primerDiaMesAct = new DateTime(fechaMesAct.Year, fechaMesAct.Month, 1);
            DateTime ultimoDiaMesAct = primerDiaMesAct.AddMonths(1).AddDays(-1);

            var response2 = await client.GetStringAsync("http://dmrbolivia.online/api/inventarios/listaInventario.php");
            var queryInv = JsonConvert.DeserializeObject<List<Datos.Inventario>>(response2);
            int cantInv = 0;
            int cantInvACT = 0;
            foreach (var iteminv in queryInv)
            {
                if (iteminv.fecha_inv >= primerDiaMesAnt)
                {
                    if (iteminv.fecha_inv <= ultimoDiaMesAnt)
                    {
                        cantInv = cantInv + Convert.ToInt32(iteminv.salida_fisica);
                    }
                }

                if (iteminv.fecha_inv >= primerDiaMesAct)
                {
                    if (iteminv.fecha_inv <= ultimoDiaMesAct)
                    {
                        cantInvACT = cantInvACT + Convert.ToInt32(iteminv.salida_fisica);
                    }
                }
            }

            txtfechaMesAnterior.Text = cantInv.ToString();
            txtfechaMesActual.Text = cantInvACT.ToString();

            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            string fechaMesAnterior = ci.DateTimeFormat.GetMonthName(DateTime.Now.AddMonths(-1).Month).ToString().ToUpper();
            txtMesAnterior.Text = fechaMesAnterior;
            string fechaMesActual = ci.DateTimeFormat.GetMonthName(DateTime.Now.Month).ToString().ToUpper();
            txtMesActual.Text = fechaMesActual;

            var response3 = await client.GetStringAsync("http://dmrbolivia.online/api/inventarios/listaInventario.php");
            var queryInv1 = JsonConvert.DeserializeObject<List<Datos.Inventario>>(response3);
            decimal totalresult = 0;

            foreach (var item in queryInv1)
            {
                totalresult = totalresult + item.saldo_valorado;
            }
            string inv2 = string.Format("{0:#,0.000}", totalresult);
            inventario2.Text = inv2;

            var response4 = await client.GetStringAsync("http://dmrbolivia.online/api/ventas/listaVenta.php");
            var q_ventas = JsonConvert.DeserializeObject<List<Datos.Ventas>>(response4);

            decimal saldoresult = 0;
            foreach (var item in q_ventas)
            {
                saldoresult = saldoresult + item.saldo;
            }
            string cuentxcobrar = string.Format("{0:#,0.000}", saldoresult);
            ctaxcobrar2.Text = cuentxcobrar;

            decimal ttl2 = totalresult + saldoresult;
            string total_2 = string.Format("{0:#,0.000}", ttl2);
            total2.Text = total_2;

            var response5 = await client.GetStringAsync("http://dmrbolivia.online/api/egresos/listaCostoFijo.php");
            var q_costoFijo = JsonConvert.DeserializeObject<List<Costo_fijo>>(response5);

            var response6 = await client.GetStringAsync("http://dmrbolivia.online/api/egresos/listaCostoVariable.php");
            var q_costoVariable = JsonConvert.DeserializeObject<List<Costo_variable>>(response6);

            decimal costoFijo = 0;
            decimal costosVariab = 0;

            foreach (var itemC in q_costoFijo)
            {
                costoFijo = costoFijo + itemC.monto;
            }
            foreach (var itemV in q_costoVariable)
            {
                if (itemV.fecha_mes == fechaMesActual)
                {
                    costosVariab = costosVariab + itemV.monto;
                }
            }
            decimal cuentxpagar = costoFijo + costosVariab;
            string cuentaxpagar = string.Format("{0:#,0.000}", cuentxpagar);
            ctaxpagar2.Text = cuentaxpagar;

            decimal efectivo_venta = 0;
            decimal efectivo = 0;
            foreach (var itemE in q_ventas)
            {
                efectivo_venta = efectivo_venta + (itemE.total - itemE.saldo);
            }
            efectivo = efectivo_venta - (costoFijo + costosVariab);
            string efect = string.Format("{0:#,0.000}", efectivo);
            txtefectivo2.Text = efect;


        }
	}
}