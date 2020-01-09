using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using ABM.Datos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ABM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaProducto : ContentPage
	{
		public ListaProducto ()
		{
			InitializeComponent ();
		}
        
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarProducto());
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/productos/listaProducto.php");
            var productos = JsonConvert.DeserializeObject<List<Datos.Producto>>(response);

            listaProd.ItemsSource = productos;
        }

        private async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            var detalles = e.Item as Producto;
            await Navigation.PushAsync(new EditarBorrarProducto(detalles.id_producto, detalles.nombre, detalles.nombre_tipo_producto, detalles.nombre_sub_producto, detalles.stock,
                detalles.stock_valorado, detalles.promedio, detalles.precio_venta, detalles.producto_alerta));
        }

        private void BtnImgTPSP_Clicked(object sender, EventArgs e)
        {
            if (btnRedirecTP.IsVisible == false)
            {
                btnRedirecTP.IsVisible = true;
            }
            else
            {
                btnRedirecTP.IsVisible = false;
            }

            if(txtRedirecTP.IsVisible == false)
            {
                txtRedirecTP.IsVisible = true;
            }
            else
            {
                txtRedirecTP.IsVisible = false;
            }

            if(btnRedirecSP.IsVisible == false)
            {
                btnRedirecSP.IsVisible = true;
            }
            else
            {
                btnRedirecSP.IsVisible = false;
            }

            if(txtRedirecSP.IsVisible == false)
            {
                txtRedirecSP.IsVisible = true;
            }
            else
            {
                txtRedirecSP.IsVisible = false;
            }
        }

        private void BtnRedirecTP_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListaTipoProducto());
        }

        private void BtnRedirecSP_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListaSubProducto());
        }

        
    }
}
