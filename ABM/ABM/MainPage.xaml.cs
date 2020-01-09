using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ABM.Forms;
using ABM.Datos;
using System.Diagnostics;

namespace ABM
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        private void BtnRedirecTipoP_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListaTipoProducto());
        }

        private void BtnRedirecSubP_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListaSubProducto());
        }

        private void BtnRedirecProd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListaProducto());
        }

        private void BtnRedirecCliente_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Forms.Cliente.ListaCliente());
        }

        private void BtnRedirecVendedor_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Forms.Vendedor.ListaVendedor());
        }

        private void BtnRedirecVenta_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new Forms.Venta.ListaVenta());
        }

        private void BtnRedirecDetalleVenta_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Forms.Venta.ListaDetalleVenta());
        }

        private void BtnRedirectInventarioGeneral_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Forms.Inventario.InventarioGeneral());
        }

        private void BtnRedirecIndex_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Index());
        }

        private void BtnRedirectCostoFijo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Forms.Egresos.TotalEgresos());
        }

        private void BtnRedirectCostoVariable_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Forms.Egresos.ListaCostoVariable());
        }
    }
}
