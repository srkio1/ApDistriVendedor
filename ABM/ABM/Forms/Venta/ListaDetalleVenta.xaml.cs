using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABM.Datos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ABM.Forms.Venta
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaDetalleVenta : ContentPage
	{
		public ListaDetalleVenta ()
		{
			InitializeComponent ();
		}

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarProducto());
        }

        protected override void OnAppearing()
        {
            
        }

        private async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            //
        }
    }
}