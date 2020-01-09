﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using ABM.Datos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ABM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AgregarTipoProducto : ContentPage
	{
		public AgregarTipoProducto ()
		{
			InitializeComponent ();
		}

        private async void BtnGuardarTP_Clicked(object sender, EventArgs e)
        {
            Tipo_producto tipo_Producto = new Tipo_producto()
            {
                id_tipoproducto = Convert.ToInt32(idproductoentry.Text),
                nombre_tipo_producto = nombreTpEntry.Text
            };

            var json = JsonConvert.SerializeObject(tipo_Producto);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var result = await client.PostAsync("http://dmrbolivia.online/api/tipoproductos/agregarTipoproducto.php", content);

            if (result.StatusCode == HttpStatusCode.Created)
            {
                await DisplayAlert("Hey", "Se agrego correctamente", "Posi mi gresan");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Hey", result.StatusCode.ToString(), "Fale Ferga");
                await Navigation.PopAsync();
            }
        }
    }
}