using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABM.Datos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ABM.Forms.Inventario
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VistaKardex : ContentPage
	{
		public VistaKardex (int Id_inventario, string Nombre_producto, DateTime Fecha_inv, int Numero_factura, string Detalle, decimal Precio_compra,
            decimal Unidades, decimal Entrada_fisica, decimal Salida_fisica, decimal Saldo_fisica, decimal Entrada_valorado, decimal Salida_valorado, decimal Saldo_valorado, decimal Promedio)
		{
			InitializeComponent ();
            idInvEntry.Text = Id_inventario.ToString();
            nombre_productoEntry.Text = Nombre_producto;
            fecha_inv.Text = Fecha_inv.ToString();
            numero_factura.Text = Numero_factura.ToString();
            detalleEntry.Text = Detalle;
            precio_compraEntry.Text = Precio_compra.ToString();
            unidadesEntry.Text = Unidades.ToString();
            entrada_fiscaEntry.Text = Entrada_fisica.ToString();
            salida_fisicaEntry.Text = Salida_fisica.ToString();
            saldo_fisicaEntry.Text = Saldo_fisica.ToString();
            entrada_valoradoEntry.Text = Entrada_valorado.ToString("{0:0.000}");
            salida_valoradoEntry.Text = Salida_valorado.ToString("{0:0.000}");
            saldo_valoradoEnry.Text = Saldo_valorado.ToString("{0:0.000}");
            //promedioEntry.Text = string.Format("{0:0.###}", Promedio.ToString());
		}
	}
}