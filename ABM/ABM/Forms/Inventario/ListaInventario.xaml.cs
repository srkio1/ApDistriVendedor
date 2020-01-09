using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABM.Datos;
using ABM.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ABM.Forms.Inventario
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaInventario : ContentPage
	{
        private string nombreProd;
		public ListaInventario (int id_venta, string Nombre, string Nombre_sub_producto, string Nombre_tipo_producto)
		{
			InitializeComponent ();

            nombreProd = Nombre_tipo_producto + " " + Nombre + " " + Nombre_sub_producto;
		}

  

        protected override void OnAppearing()
        {
            base.OnAppearing();

            
        }
    }
}