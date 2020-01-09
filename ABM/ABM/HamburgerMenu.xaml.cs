using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ABM;
using ABM.Forms.Finanzas;

namespace HamburgerMenu
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HamburgerMenu : MasterDetailPage
    {
        private string nombreVendedor;
		public HamburgerMenu (string NombreVendedor)
		{
            nombreVendedor = NombreVendedor;
			InitializeComponent ();
            MyMenu();
            MasterBehavior = MasterBehavior.SplitOnLandscape;
            txtMenu.Text = nombreVendedor;
        }

        public void MyMenu()
        {
            Detail = new NavigationPage(new ABM.Forms.Index());
            
            List<Menu> menu = new List<Menu>
            {
                
                new Menu{ Page= new ABM.Forms.Index(), MenuTitle="MainPage", icon="icon_mainpage.png"},
                new Menu{ Page= new ABM.Forms.Inventario.InventarioGeneral(), MenuTitle="Inventario", icon="icon_inventario.png"},
                new Menu{ Page= new ABM.Forms.Venta.ListaVenta(nombreVendedor), MenuTitle="Venta", icon="icon_venta.png"},
                new Menu{ Page= new ABM.Forms.Compra.ListaCompra(), MenuTitle="Compra", icon="icon_compra.png"},
                //new Menu{ Page= new ListaProducto(), MenuTitle="Producto", icon="icon_producto.png"},
                new Menu{ Page= new ABM.Forms.Cliente.ListaCliente(), MenuTitle="Cliente", icon="icon_cliente.png"},                
                new Menu{ Page= new ABM.Forms.Vendedor.ListaVendedor(), MenuTitle="Vendedor", icon="icon_vendedor.png"},
                //new Menu{ Page= new ABM.Forms.Proveedor.ListaProveedor(), MenuTitle="Proveedor", icon="icon_proveedor.png"},
                new Menu{ Page= new ABM.Forms.Finanzas.ListaFinanzas(), MenuTitle = "Finanzas", icon="fina.png"},
                //new Menu{ Page= new ABM.Forms.Egresos.ListaCostoFijo(), MenuTitle="Egresos fijos", icon="icon_egresos.png"},
                new Menu{ Page= new ABM.Forms.Egresos.TotalEgresos(), MenuTitle="Total Egresos", icon="fina.png"},
                new Menu{ Page= new ABM.Forms.Agenda.Listaagenda(), MenuTitle="Agenda", icon="agendaicono.png"},
                //new Menu{ Page= new MainPage(), MenuTitle="MainPage", icon="icon_proveedor.png"}
            };
            ListMenu.ItemsSource = menu;
        }
        private void ListMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var menu = e.SelectedItem as Menu;
            if (menu != null)
            {
                IsPresented = false;
                Detail = new NavigationPage(menu.Page);
            }
        }
        public class Menu
        {
            public string MenuTitle
            {
                get;
                set;
            }
            public string MenuDetail
            {
                get;
                set;
            }

            public ImageSource icon
            {
                get;
                set;
            }

            public Page Page
            {
                get;
                set;
            }
        }
	}
}