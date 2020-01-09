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


namespace ABM.Forms.Compra
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MostrarCompra : ContentPage
	{
        ObservableCollection<DetalleCompra> detalleCompra_lista = new ObservableCollection<DetalleCompra>();
        public ObservableCollection<DetalleCompra> DetallesCompras { get { return detalleCompra_lista; } }

        private int facturacomp = 0;
        private int ID_COMPRA = 0;
        private decimal SALDO = 0;
        private decimal TOTAL = 0;
        private DateTime FECHA_COMPRA;
        private int NUMERO_FACTURA = 0;
        private string PROVEEDOR;

        public MostrarCompra(int id_compra, decimal saldo,decimal total, DateTime fecha_compra, DateTime hora, int numero_factura, string proveedor)
        {
            InitializeComponent();

            idCompraEntry.Text = id_compra.ToString();
            facturaEntry.Text = numero_factura.ToString();
            facturacomp = numero_factura;
            fechaEntry.Text = fecha_compra.ToString("dd/MM/yyyy") + " - " + hora.ToString("hh:mm");            
            proveedorEntry.Text = proveedor;            
            totalEntry.Text = total.ToString();

            ID_COMPRA = id_compra;
            SALDO = saldo;
            TOTAL = total;
            FECHA_COMPRA = fecha_compra;
            NUMERO_FACTURA = numero_factura;
            PROVEEDOR = proveedor;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/compras/listaDetalleCompra.php");
            var costos_variables = JsonConvert.DeserializeObject<List<DetalleCompra>>(response);
                                 

            int cont = costos_variables.Count;
            int numProd = 0;
            foreach (var item in costos_variables)
            {
                if (facturacomp == item.numero_factura)
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
                    entCant.Text = item.cantidad_compra.ToString();
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
                    entdesc.Text = item.descuento_producto.ToString();
                    entdesc.FontSize = 23;
                    entdesc.HorizontalOptions = LayoutOptions.FillAndExpand;
                    stkP4.Children.Add(entdesc);                   
                }
            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new EditarBorrarCompra(ID_COMPRA,SALDO,TOTAL,FECHA_COMPRA,NUMERO_FACTURA,PROVEEDOR));
        }
    }
}

