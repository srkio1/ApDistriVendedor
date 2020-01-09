using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace ABM.Forms.Inventario
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Kardex : ContentPage
	{
        private string nombre_producto;
        ObservableCollection<Datos.Inventario> inventario_list = new ObservableCollection<Datos.Inventario>();
        public ObservableCollection<Datos.Inventario> Inventarios { get { return inventario_list; } }
        public Kardex(string nombre, string nombre_sub_producto, string nombre_tipo_producto)
        {
            InitializeComponent();
            nombre_producto = nombre_tipo_producto + " " + nombre + " " + nombre_sub_producto;

        }
        protected async override void OnAppearing()
        {

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/inventarios/listaInventario.php");
            var kardex = JsonConvert.DeserializeObject<List<Datos.Inventario>>(response);

            foreach (var item in kardex)
            {
                if (nombre_producto == item.nombre_p)
                {
                    inventario_list.Add(new Datos.Inventario
                    {
                        id_inventario = item.id_inventario,
                        nombre_p = item.nombre_p,
                        fecha_inv = item.fecha_inv,
                        numero_factura = item.numero_factura,
                        detalle = item.detalle,
                        precio_compra = item.precio_compra,

                        unidades = item.unidades,
                        entrada_fisica = item.entrada_fisica,
                        salida_fisica = item.salida_fisica,
                        saldo_fisica = item.saldo_fisica,
                        entrada_valorado = item.entrada_valorado,
                        salida_valorado = item.salida_valorado,
                        saldo_valorado = item.saldo_valorado,
                        promedio = item.promedio
                    });
                }
            }

            listInventario.ItemsSource = inventario_list.OrderBy(w => w.id_inventario);

        }
    }
}