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
using System.Collections.ObjectModel;

namespace ABM.Forms.Venta
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MostrarVenta : ContentPage
	{
        ObservableCollection<DetalleVenta> detalleVenta_lista = new ObservableCollection<DetalleVenta>();
        public ObservableCollection<DetalleVenta> DetallesVentas { get { return detalleVenta_lista; } }

        private int facturacomp = 0;

        private int ID_VENTA = 0;
        private DateTime FECHA;
        private int NUMERO_FACTURA = 0;
        private string CLIENTE;
        private string VENDEDOR;
        private string TIPO_VENTA;
        private decimal DESCUENTO = 0;
        private decimal SALDO = 0;
        private decimal TOTAL = 0;

        public MostrarVenta(int id_venta, DateTime fecha, DateTime hora, int numero_factura, string cliente, string vendedor, string tipo_venta, decimal descuento, decimal saldo, decimal total)
        {
            InitializeComponent();

            idVentaEntry.Text = Id.ToString();
            facturaEntry.Text = numero_factura.ToString();
            facturacomp = numero_factura;
            fechaEntry.Text = fecha.ToString("dd/MM/yyyy") +" - "+ hora.ToString("hh:mm:ss");
            tipoVentaEntry.Text = tipo_venta;
            vendedorEntry.Text = vendedor;
            clienteEntry.Text = cliente;
            totalEntry.Text = total.ToString();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/ventas/listaDetalleVenta.php");
            var costos_variables = JsonConvert.DeserializeObject<List<DetalleVenta>>(response);




            int cont = costos_variables.Count;
            int numProd = 0;
            foreach (var item in costos_variables)
            {
                if (facturacomp == item.factura)
                {
                    BoxView boxView = new BoxView();
                    boxView.HeightRequest = 1;
                    boxView.BackgroundColor = Color.Black;
                    stkPrd.Children.Add(boxView);

                    numProd = numProd + 1;
                    StackLayout stkP1 = new StackLayout();
                    stkP1.Orientation = StackOrientation.Horizontal;
                    stkPrd.Children.Add(stkP1);


                    Label label1 = new Label();
                    label1.Text = "Producto " + numProd.ToString();
                    label1.FontSize = 23;
                    label1.TextColor = Color.Blue;
                    label1.WidthRequest = 200;
                    stkP1.Children.Add(label1);
                    Label entNomProd = new Label();
                    entNomProd.Text = item.nombre_producto;
                    entNomProd.FontSize = 23;
                    entNomProd.HorizontalOptions = LayoutOptions.FillAndExpand;
                    stkP1.Children.Add(entNomProd);


                    StackLayout stkP2 = new StackLayout();
                    stkP2.Orientation = StackOrientation.Horizontal;
                    stkPrd.Children.Add(stkP2);

                    Label label2 = new Label();
                    label2.Text = "Cantidad";
                    label2.FontSize = 23;
                    label2.TextColor = Color.Blue;
                    label2.WidthRequest = 200;
                    stkP2.Children.Add(label2);
                    Label entCant = new Label();
                    entCant.Text = item.cantidad.ToString();
                    entCant.FontSize = 23;
                    entCant.HorizontalOptions = LayoutOptions.FillAndExpand;
                    stkP2.Children.Add(entCant);

                    StackLayout stkP3 = new StackLayout();
                    stkP3.Orientation = StackOrientation.Horizontal;
                    stkPrd.Children.Add(stkP3);

                    Label label3 = new Label();
                    label3.Text = "Precio";
                    label3.FontSize = 23;
                    label3.TextColor = Color.Blue;
                    label3.WidthRequest = 200;
                    stkP3.Children.Add(label3);
                    Label entPrec = new Label();
                    entPrec.Text = item.precio_producto.ToString();
                    entPrec.FontSize = 23;
                    entPrec.HorizontalOptions = LayoutOptions.FillAndExpand;
                    stkP3.Children.Add(entPrec);

                    StackLayout stkP4 = new StackLayout();
                    stkP4.Orientation = StackOrientation.Horizontal;
                    stkPrd.Children.Add(stkP4);

                    Label label4 = new Label();
                    label4.Text = "Descuento";
                    label4.FontSize = 23;
                    label4.TextColor = Color.Blue;
                    label4.WidthRequest = 200;
                    stkP4.Children.Add(label4);
                    Label entdesc = new Label();
                    entdesc.Text = item.descuento.ToString();
                    entdesc.FontSize = 23;
                    entdesc.HorizontalOptions = LayoutOptions.FillAndExpand;
                    stkP4.Children.Add(entdesc);
                    
                }

            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditarBorrarVenta(ID_VENTA, FECHA, NUMERO_FACTURA, CLIENTE, VENDEDOR,
                TIPO_VENTA, DESCUENTO, SALDO , TOTAL));
        }
    }
}

