using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using ABM.Datos;
using ABM.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace ABM.Forms.Inventario
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InventarioGeneral : ContentPage
	{
        private int Consult = 0;
        private int valConsultMarca = 1;
        private int valConsutCalibre = 1;
        private int valConsultStock = 1;
        private int valConsultPromedio = 1;
        private int valConsultStockValorado = 1;
        public InventarioGeneral()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/productos/listaProducto.php");
            var productos = JsonConvert.DeserializeObject<List<Producto>>(response);
            listInventarioGeneral.ItemsSource = productos;
            
            int canTotal = 0;
            decimal SumTotal = 0;
            foreach (var item in productos)
            {
                canTotal = canTotal + Convert.ToInt32(item.stock);
                SumTotal = SumTotal + item.stock_valorado;
            }
            string cntSm = string.Format("{0:#,0}", canTotal);
            string bssm = string.Format("{0:#,0.000}", SumTotal);
            txtCantSum.Text = cntSm;
            txtBsSum.Text = bssm;
        }

        private async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            var detalles = e.Item as Producto;
            await Navigation.PushAsync(new Kardex(detalles.nombre, detalles.nombre_sub_producto, detalles.nombre_tipo_producto));
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/productos/listaProducto.php");
            var productos = JsonConvert.DeserializeObject<List<Producto>>(response);
            valConsultMarca = valConsultMarca + 1;
            if(valConsultMarca % 2 == 0)
            {
                listInventarioGeneral.ItemsSource = productos.OrderByDescending(w => w.nombre);
            }
            else
            {
                listInventarioGeneral.ItemsSource = productos.OrderBy(w => w.nombre);
            }
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/productos/listaProducto.php");
            var productos = JsonConvert.DeserializeObject<List<Producto>>(response);
            valConsutCalibre = valConsutCalibre + 1;
            if(valConsutCalibre % 2 == 0)
            {
                listInventarioGeneral.ItemsSource = productos.OrderByDescending(w => w.nombre_sub_producto);
            }
            else
            {
                listInventarioGeneral.ItemsSource = productos.OrderBy(w => w.nombre_sub_producto);
            }
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/productos/listaProducto.php");
            var productos = JsonConvert.DeserializeObject<List<Producto>>(response);
            valConsultStock = valConsultStock + 1;
            if(valConsultStock % 2 == 0)
            {
                listInventarioGeneral.ItemsSource = productos.OrderByDescending(w => w.stock);
            }
            else
            {
                listInventarioGeneral.ItemsSource = productos.OrderBy(w => w.stock);
            }
            
        }

        private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/productos/listaProducto.php");
            var productos = JsonConvert.DeserializeObject<List<Producto>>(response);
            valConsultPromedio = valConsultPromedio + 1;
            if(valConsultPromedio % 2 == 0)
            {
                listInventarioGeneral.ItemsSource = productos.OrderByDescending(w => w.promedio);
            }
            else
            {
                listInventarioGeneral.ItemsSource = productos.OrderBy(w => w.promedio);
            }
        }

        private async void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/productos/listaProducto.php");
            var productos = JsonConvert.DeserializeObject<List<Producto>>(response);
            valConsultStockValorado = valConsultStockValorado + 1;
            if(valConsultStockValorado % 2 == 0)
            {
                listInventarioGeneral.ItemsSource = productos.OrderByDescending(w => w.stock_valorado);
            }
            else
            {
                listInventarioGeneral.ItemsSource = productos.OrderBy(w => w.stock_valorado);
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarKardex());
        }
    }
}