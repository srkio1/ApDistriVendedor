using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using Newtonsoft.Json;
using ABM.Datos;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net;

namespace ABM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarProducto : ContentPage
    {
        public AgregarProducto()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();



            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/tipoproductos/listaTipoproducto.php");
            var tipoproductos = JsonConvert.DeserializeObject<List<Datos.Tipo_producto>>(response);

            tpPicker.ItemsSource = tipoproductos;

           
            var response1 = await client.GetStringAsync("http://dmrbolivia.online/api/subproductos/listaSubproducto.php");
            var subproducto = JsonConvert.DeserializeObject<List<Datos.Sub_producto>>(response1);

            spPicker.ItemsSource = subproducto;
            
        }
        private string pickTP;
        private void TpPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                pickTP = picker.Items[selectedIndex];
            }
        }
        private string pickSP;
        private void SpPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                pickSP = picker.Items[selectedIndex];
            }
        }
     
        

             
       
    private async void BtnGuardarPr_Clicked(object sender, EventArgs e)
        {
            try
            {
                Producto producto = new Producto()
                {
                   // id_producto = Convert.ToInt32(idEntry.Text),
                    nombre = nombrePEntry.Text,
                    nombre_tipo_producto = pickTP,
                    nombre_sub_producto = pickSP,
                    stock = Convert.ToDecimal(stockProductoEntry.Text),
                    stock_valorado = Convert.ToDecimal(stockValoradoProductoEntry.Text),
                    promedio = Convert.ToDecimal(promedioProductoEntry.Text),
                    precio_venta = Convert.ToDecimal(precioventaEntry.Text),

                    producto_alerta = Convert.ToDecimal(alertaProductoEntry.Text)
                };

                var json = JsonConvert.SerializeObject(producto);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();

                var result = await client.PostAsync("http://dmrbolivia.online/api/productos/agregarProducto.php", content);

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    await DisplayAlert("Guardado", "Se agrego correctamente", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", result.StatusCode.ToString(), "OK");
                    await Navigation.PopAsync();
                }
            }
            catch (Exception error)
            {
                await DisplayAlert("ERROR", error.ToString(), "OK");
            }
        }             
        
    }
}