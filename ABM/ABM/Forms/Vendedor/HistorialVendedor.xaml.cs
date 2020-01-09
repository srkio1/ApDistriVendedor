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

namespace ABM.Forms.Vendedor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistorialVendedor : ContentPage
    {
        List<Ventas> Items;
        private string nombreVendedor;
        public HistorialVendedor(string nombre_vendedor)
        {
            InitializeComponent();
            nombreVendedor = nombre_vendedor;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Items = new List<Ventas>();
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("http://dmrbolivia.online/api/ventas/listaVenta.php");
                var venta = JsonConvert.DeserializeObject<List<Ventas>>(response);
                foreach (var item in venta)
                {
                    if(item.vendedor == nombreVendedor)
                    {
                        Items.Add(new Ventas
                        {
                            id_venta = item.id_venta,
                            fecha = item.fecha,
                            numero_factura = item.numero_factura,
                            cliente = item.cliente,
                            vendedor = item.vendedor,
                            tipo_venta = item.tipo_venta,
                            descuento = item.descuento,
                            saldo = item.saldo,
                            total = item.total
                        });
                    }
                }
            }
            catch (Exception err)
            {
                await DisplayAlert("ERROR", err.ToString(), "OK");
            }
            listaVendedorH.ItemsSource = Items;
        }

        private async void ListaVendedorH_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var detalles = e.Item as Ventas;
            await Navigation.PushAsync(new Venta.MostrarVenta(detalles.id_venta, detalles.fecha, detalles.hora, detalles.numero_factura, detalles.cliente,
                                                        detalles.vendedor, detalles.tipo_venta, detalles.descuento, detalles.saldo, detalles.total));
        }

    }
}