using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ABM.Datos;
using ABM.Forms.Egresos;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ABM.Forms.Egresos
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TotalEgresos : ContentPage
	{
        List<Costo_fijo> Items;
        List<CostoVariable> Items2;
        public TotalEgresos ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Items = new List<Costo_fijo>();
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("http://dmrbolivia.online/api/egresos/listaCostoFijo.php");
                var costoFijo = JsonConvert.DeserializeObject<List<Costo_fijo>>(response);
                foreach (var item in costoFijo)
                {
                    Items.Add(new Costo_fijo
                    {
                        id_costo_fijo = item.id_costo_fijo,
                        nombre = item.nombre,
                        monto = item.monto,
                        fecha_year = item.fecha_year
                    });
                        
                }
            }
            catch (Exception err)
            {
                await DisplayAlert("ERROR", err.ToString(), "OK");
            }
            listEgresos.ItemsSource = Items;

            Items2 = new List<CostoVariable>();
            try
            {
                HttpClient client = new HttpClient();
                var response1 = await client.GetStringAsync("http://dmrbolivia.online/api/egresos/listaCostoVariable.php");
                var costoVariable = JsonConvert.DeserializeObject<List<Costo_variable>>(response1);
                foreach (var item in costoVariable)
                {
                    if(item.fecha_mes == "Enero")
                    {
                        Items2.Add(new CostoVariable
                        {
                            nombre = item.nombre,
                            monto_enero = item.monto,
                            monto_febrero = 0,
                            monto_marzo = 0,
                            monto_abril = 0,
                            monto_mayo = 0,
                            monto_junio = 0,
                            monto_julio = 0,
                            monto_agosto = 0,
                            monto_septiembre = 0,
                            monto_octubre = 0,
                            monto_noviembre = 0,
                            monto_diciembre = 0
                        });
                    }
                    else if(item.fecha_mes == "Febrero")
                    {
                        Items2.Add(new CostoVariable
                        {
                            nombre = item.nombre,
                            monto_enero = 0,
                            monto_febrero = item.monto,
                            monto_marzo = 0,
                            monto_abril = 0,
                            monto_mayo = 0,
                            monto_junio = 0,
                            monto_julio = 0,
                            monto_agosto = 0,
                            monto_septiembre = 0,
                            monto_octubre = 0,
                            monto_noviembre = 0,
                            monto_diciembre = 0
                        });
                    }
                    else if (item.fecha_mes == "Marzo")
                    {
                        Items2.Add(new CostoVariable
                        {
                            nombre = item.nombre,
                            monto_enero = 0,
                            monto_febrero = 0,
                            monto_marzo = item.monto,
                            monto_abril = 0,
                            monto_mayo = 0,
                            monto_junio = 0,
                            monto_julio = 0,
                            monto_agosto = 0,
                            monto_septiembre = 0,
                            monto_octubre = 0,
                            monto_noviembre = 0,
                            monto_diciembre = 0
                        });
                    }
                    else if (item.fecha_mes == "Abril")
                    {
                        Items2.Add(new CostoVariable
                        {
                            nombre = item.nombre,
                            monto_enero = 0,
                            monto_febrero = 0,
                            monto_marzo = 0,
                            monto_abril = item.monto,
                            monto_mayo = 0,
                            monto_junio = 0,
                            monto_julio = 0,
                            monto_agosto = 0,
                            monto_septiembre = 0,
                            monto_octubre = 0,
                            monto_noviembre = 0,
                            monto_diciembre = 0
                        });
                    }
                    else if (item.fecha_mes == "Mayo")
                    {
                        Items2.Add(new CostoVariable
                        {
                            nombre = item.nombre,
                            monto_enero = 0,
                            monto_febrero = 0,
                            monto_marzo = 0,
                            monto_abril = 0,
                            monto_mayo = item.monto,
                            monto_junio = 0,
                            monto_julio = 0,
                            monto_agosto = 0,
                            monto_septiembre = 0,
                            monto_octubre = 0,
                            monto_noviembre = 0,
                            monto_diciembre = 0
                        });
                    }
                    else if (item.fecha_mes == "Junio")
                    {
                        Items2.Add(new CostoVariable
                        {
                            nombre = item.nombre,
                            monto_enero = 0,
                            monto_febrero = 0,
                            monto_marzo = 0,
                            monto_abril = 0,
                            monto_mayo = 0,
                            monto_junio = item.monto,
                            monto_julio = 0,
                            monto_agosto = 0,
                            monto_septiembre = 0,
                            monto_octubre = 0,
                            monto_noviembre = 0,
                            monto_diciembre = 0
                        });
                    }
                    else if (item.fecha_mes == "Julio")
                    {
                        Items2.Add(new CostoVariable
                        {
                            nombre = item.nombre,
                            monto_enero = 0,
                            monto_febrero = 0,
                            monto_marzo = 0,
                            monto_abril = 0,
                            monto_mayo = 0,
                            monto_junio = 0,
                            monto_julio = item.monto,
                            monto_agosto = 0,
                            monto_septiembre = 0,
                            monto_octubre = 0,
                            monto_noviembre = 0,
                            monto_diciembre = 0
                        });
                    }
                    else if (item.fecha_mes == "Agosto")
                    {
                        Items2.Add(new CostoVariable
                        {
                            nombre = item.nombre,
                            monto_enero = 0,
                            monto_febrero = 0,
                            monto_marzo = 0,
                            monto_abril = 0,
                            monto_mayo = 0,
                            monto_junio = 0,
                            monto_julio = 0,
                            monto_agosto = item.monto,
                            monto_septiembre = 0,
                            monto_octubre = 0,
                            monto_noviembre = 0,
                            monto_diciembre = 0
                        });
                    }
                    else if (item.fecha_mes == "Septiembre")
                    {
                        Items2.Add(new CostoVariable
                        {
                            nombre = item.nombre,
                            monto_enero = 0,
                            monto_febrero = 0,
                            monto_marzo = 0,
                            monto_abril = 0,
                            monto_mayo = 0,
                            monto_junio = 0,
                            monto_julio = 0,
                            monto_agosto = 0,
                            monto_septiembre = item.monto,
                            monto_octubre = 0,
                            monto_noviembre = 0,
                            monto_diciembre = 0
                        });
                    }
                    else if (item.fecha_mes == "Octubre")
                    {
                        Items2.Add(new CostoVariable
                        {
                            nombre = item.nombre,
                            monto_enero = 0,
                            monto_febrero = 0,
                            monto_marzo = 0,
                            monto_abril = 0,
                            monto_mayo = 0,
                            monto_junio = 0,
                            monto_julio = 0,
                            monto_agosto = 0,
                            monto_septiembre = 0,
                            monto_octubre = item.monto,
                            monto_noviembre = 0,
                            monto_diciembre = 0
                        });
                    }
                    else if (item.fecha_mes == "Noviembre")
                    {
                        Items2.Add(new CostoVariable
                        {
                            nombre = item.nombre,
                            monto_enero = 0,
                            monto_febrero = 0,
                            monto_marzo = 0,
                            monto_abril = 0,
                            monto_mayo = 0,
                            monto_junio = 0,
                            monto_julio = 0,
                            monto_agosto = 0,
                            monto_septiembre = 0,
                            monto_octubre = 0,
                            monto_noviembre = item.monto,
                            monto_diciembre = 0
                        });
                    }
                    else if (item.fecha_mes == "Diciembre")
                    {
                        Items2.Add(new CostoVariable
                        {
                            nombre = item.nombre,
                            monto_enero = 0,
                            monto_febrero = 0,
                            monto_marzo = 0,
                            monto_abril = 0,
                            monto_mayo = 0,
                            monto_junio = 0,
                            monto_julio = 0,
                            monto_agosto = 0,
                            monto_septiembre = 0,
                            monto_octubre = 0,
                            monto_noviembre = 0,
                            monto_diciembre = item.monto
                        });
                    }
                }
            }
            catch (Exception err)
            {
                await DisplayAlert("ERROR", err.ToString(), "OK");
            }
            listEgresos2.ItemsSource = Items2;
        }
    }
}