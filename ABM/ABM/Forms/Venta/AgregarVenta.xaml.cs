﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABM.Datos;
using ABM.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ABM.Forms.Venta
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AgregarVenta : ContentPage
	{
        private string NombreVendedor;
        List<string> entries = new List<string>();
        public AgregarVenta(string Nombrevendedor)
        {
            
            try
            {
                InitializeComponent();

            }
            catch (Exception er)
            { Console.Write(er); }
            NombreVendedor = Nombrevendedor;
            vendedorPicker.Text = NombreVendedor;
            GetDataCliente();
            //GetDataVendedor();
        }
        private async void CampoVenta()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/ventas/listaVenta.php");
            var ventas = JsonConvert.DeserializeObject<List<Datos.Ventas>>(response);
        }
        private async void GetDataCliente()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://dmrbolivia.online/api/clientes/listaCliente.php");
            var clientes = JsonConvert.DeserializeObject<List<Datos.Cliente>>(response).ToList();
            clientePicker.ItemsSource = clientes;
        }
        //private async void GetDataVendedor()
        //{
        //    try
        //    {
        //        HttpClient client = new HttpClient();
        //        var response = await client.GetStringAsync("http://dmrbolivia.online/api/vendedores/listaVendedores.php");
        //        var vendedores = JsonConvert.DeserializeObject<List<Vendedores>>(response).ToList();
        //        vendedorPicker.ItemsSource = vendedores;
        //    }
        //    catch (Exception error)
        //    {
        //        await DisplayAlert("Erro", error.ToString(), "OK");
        //    }
        //}

        private int contcampos = 0;
        private string pickTipoProdChanged;
        private string pickProdNomChanged;
        private decimal MontoTotal = 0;
        private decimal Precio = 0;
        private decimal Cantidad = 0;
        private decimal Descuento = 0;
        private decimal Subtotal = 0;
        int contador = 0;
        int IdStkPrimer = 0;
        int IdStkSegundo = 0;
        int IdStkF1 = 0;
        int IdStkFV1 = 0;
        int IdStkFB1 = 0;
        int IdStkF2 = 0;
        int IdStkF3 = 0;
        int IdPickTP = 0;
        int IdPickPR = 0;
        int IdEntSubtotal = 0;
        int IdEntPrecio = 0;
        int IdEntDesc = 0;
        int IdEntCant = 0;
        int contPosicion = 0;
        string[] tiProArr = new string[20];
        string[] prodArr = new string[20];
        string[] precioArr = new string[20];
        string[] cantArr = new string[20];
        string[] descArr = new string[20];
        string[] subtArr = new string[20];
        string[] subProdArr = new string[20];
        string[] idProdArr = new string[20];
        string[] nombreProdArr = new string[20];
        string[] promedioArr = new string[20];
        string[] stockArr = new string[20];
        string[] stockValoradoArr = new string[20];
        string[] precioVentArr = new string[20];

        string[] arrayColor = { "#CED8F6", "#FFFFFF", "#CED8F6", "#FFFFFF", "#CED8F6", "#FFFFFF", "#CED8F6", "#FFFFFF", "#CED8F6", "#FFFFFF" };

        private decimal PrecioPrueba = 0;

        public async void NuevoProducto()
        {
            try
            {
                HttpClient client = new HttpClient();
                var response4 = await client.GetStringAsync("http://dmrbolivia.online/api/tipoproductos/listaTipoproducto.php");
                var tipo_productos = JsonConvert.DeserializeObject<List<Tipo_producto>>(response4).ToList();
                var response3 = await client.GetStringAsync("http://dmrbolivia.online/api/productos/listaProducto.php");
                var productos = JsonConvert.DeserializeObject<List<Producto>>(response3).ToList();

                contador = contador + 1;

                StackLayout stkF0 = new StackLayout();
                stkF0.StyleId = IdStkF1.ToString();
                stkF0.BackgroundColor = Color.FromHex(arrayColor[contador]);
                stkProductos.Children.Add(stkF0);

                IdStkPrimer = IdStkPrimer + 1;
                StackLayout StkPrimer = new StackLayout();
                StkPrimer.StyleId = StkPrimer.ToString();
                StkPrimer.Orientation = StackOrientation.Horizontal;
                StkPrimer.HorizontalOptions = LayoutOptions.FillAndExpand;
                stkF0.Children.Add(StkPrimer);

                IdStkSegundo = IdStkSegundo + 1;
                StackLayout StkSegundo = new StackLayout();
                StkSegundo.StyleId = StkPrimer.ToString();
                StkSegundo.Orientation = StackOrientation.Horizontal;
                StkSegundo.HorizontalOptions = LayoutOptions.FillAndExpand;
                stkF0.Children.Add(StkSegundo);

                IdStkF1 = IdStkF1 + 1;
                StackLayout stkF1 = new StackLayout();
                stkF1.StyleId = IdStkF1.ToString();
                stkF1.Orientation = StackOrientation.Horizontal;
                stkF1.HorizontalOptions = LayoutOptions.FillAndExpand;
                StkPrimer.Children.Add(stkF1);

                IdStkFB1 = IdStkFB1 + 1;
                StackLayout stkFB1 = new StackLayout();
                stkFB1.StyleId = IdStkF1.ToString();
                stkFB1.Orientation = StackOrientation.Horizontal;
                stkFB1.HorizontalOptions = LayoutOptions.FillAndExpand;
                StkPrimer.Children.Add(stkFB1);

                IdStkFV1 = IdStkFV1 + 1;
                StackLayout stkFV1 = new StackLayout();
                stkFV1.StyleId = IdStkF1.ToString();
                stkFV1.Orientation = StackOrientation.Horizontal;
                stkFV1.HorizontalOptions = LayoutOptions.FillAndExpand;
                StkPrimer.Children.Add(stkFV1);

                IdStkF2 = IdStkF2 + 1;
                StackLayout stkF2 = new StackLayout();
                stkF2.StyleId = IdStkF2.ToString();
                stkF2.Orientation = StackOrientation.Horizontal;
                stkF2.HorizontalOptions = LayoutOptions.FillAndExpand;
                StkSegundo.Children.Add(stkF2);

                IdStkF3 = IdStkF3 + 1;
                StackLayout stkF3 = new StackLayout();
                stkF3.StyleId = IdStkF2.ToString();
                stkF3.Orientation = StackOrientation.Horizontal;
                stkF3.HorizontalOptions = LayoutOptions.FillAndExpand;
                StkSegundo.Children.Add(stkF3);

                IdEntPrecio = IdEntPrecio + 1;
                Entry precioEnt = new Entry();
                precioEnt.StyleId = IdEntPrecio.ToString();
                precioEnt.Placeholder = "  Precio  ";
                precioEnt.Text = 0.ToString();
                precioEnt.HorizontalOptions = LayoutOptions.FillAndExpand;
                precioEnt.Completed += MyEntryPrecioSelectedIndexChanged;
                stkFV1.Children.Add(precioEnt);

                void MyEntryPrecioSelectedIndexChanged(object sender, EventArgs e)
                {
                    var text_precio = ((Entry)sender).Text;
                    precioArr.SetValue(text_precio, contador);
                    Precio = Convert.ToDecimal(text_precio);
                }

                IdPickPR = IdPickPR + 1;
                Picker pickProdNom = new Picker();
                pickProdNom.StyleId = IdPickPR.ToString();
                pickProdNom.SetBinding(Picker.ItemsSourceProperty, "Producto");
                pickProdNom.SetBinding(Picker.SelectedItemProperty, "DisplayTextNombre");
                pickProdNom.ItemDisplayBinding = new Binding("DisplayTextNombre");
                pickProdNom.SelectedIndexChanged += MyPickProdNomSelectedIndexChanged;
                pickProdNom.HorizontalOptions = LayoutOptions.FillAndExpand;
                pickProdNom.Title = "             Producto             ";
                stkFB1.Children.Add(pickProdNom);

                void MyPickProdNomSelectedIndexChanged(object sender, EventArgs e)
                {
                    var picker = (Picker)sender;
                    int selectedIndex = picker.SelectedIndex;

                    if (selectedIndex != -1)
                    {
                        pickProdNomChanged = picker.Items[selectedIndex];
                        prodArr.SetValue(pickProdNomChanged, contador);

                    }
                    foreach (var item2 in productos)
                    {
                        if (item2.display_text_nombre == pickProdNomChanged)
                        {
                            txttest.Text = item2.promedio.ToString();
                            txttest2.Text = item2.stock.ToString();
                            txttest3.Text = item2.stock_valorado.ToString();
                            txttest4.Text = item2.id_producto.ToString();
                            precioEnt.Text = item2.precio_venta.ToString();
                            precioArr.SetValue(precioEnt.Text, contador);
                            promedioArr.SetValue(txttest.Text, contador);
                            stockArr.SetValue(txttest2.Text, contador);
                            stockValoradoArr.SetValue(txttest3.Text, contador);
                            idProdArr.SetValue(txttest4.Text, contador);
                        }
                    }
                }

                IdPickTP = IdPickTP + 1;
                Picker pickTipoProd = new Picker();
                pickTipoProd.StyleId = IdPickTP.ToString();
                pickTipoProd.SetBinding(Picker.ItemsSourceProperty, "Tipo_producto");
                pickTipoProd.SetBinding(Picker.SelectedItemProperty, "nombre_tipo_producto");
                pickTipoProd.ItemDisplayBinding = new Binding("nombre_tipo_producto");
                pickTipoProd.SelectedIndexChanged += MyPickTipoProdSelectedIndexChanged;
                pickTipoProd.HorizontalOptions = LayoutOptions.FillAndExpand;
                pickTipoProd.Title = "  Tipo producto  ";
                stkF1.Children.Add(pickTipoProd);
                pickTipoProd.ItemsSource = tipo_productos;

                void MyPickTipoProdSelectedIndexChanged(object sender, EventArgs e)
                {
                    var picker = (Picker)sender;
                    int selectedIndex = picker.SelectedIndex;

                    if (selectedIndex != -1)
                    {
                        pickTipoProdChanged = picker.Items[selectedIndex];
                        tiProArr.SetValue(pickTipoProdChanged, contador);
                        foreach (var item in productos)
                        {
                            if (item.nombre_tipo_producto == pickTipoProdChanged)
                            {
                                pickProdNom.Items.Add(item.display_text_nombre);
                            }
                        }
                        try
                        {
                            var obj = (Producto)picker.SelectedItem;
                            int ide = obj.id_producto;
                            string nombrep = obj.nombre;
                            string subproductp = obj.nombre_sub_producto;
                            decimal stockProd = obj.stock;
                            decimal stockValoradoProd = obj.stock_valorado;
                            decimal promedioProd = obj.promedio;
                            decimal precioVent = obj.precio_venta;
                            idProdArr.SetValue(ide, contador);
                            nombreProdArr.SetValue(nombrep, contador);
                            subProdArr.SetValue(subproductp, contador);
                            stockArr.SetValue(stockProd, contador);
                            stockValoradoArr.SetValue(stockValoradoProd, contador);
                            promedioArr.SetValue(promedioProd, contador);
                            precioVentArr.SetValue(precioVent, contador);
                            txttest.Text = "promedio= " + promedioArr[contador] + " | stock= " + stockArr[contador];
                        }
                        catch (Exception err)
                        {
                            Console.Write(err);
                        }
                    }
                }

                IdEntSubtotal = IdEntSubtotal + 1;
                Entry subTotalEnt = new Entry();
                subTotalEnt.StyleId = IdEntSubtotal.ToString();
                subTotalEnt.Placeholder = "  Sub Total  ";
                subTotalEnt.HorizontalOptions = LayoutOptions.Fill;
                subTotalEnt.Completed += MyEntrySubTotalSelectedIndexChanged;
                stkF3.Children.Add(subTotalEnt);

                void MyEntrySubTotalSelectedIndexChanged(object sender, EventArgs e)
                {
                    var text_subtotal = ((Entry)sender).Text;
                    Subtotal = Convert.ToDecimal(text_subtotal);
                    subtArr.SetValue(text_subtotal, contador);
                }

                IdEntDesc = IdEntDesc + 1;
                Entry descuentoEnt = new Entry();
                descuentoEnt.StyleId = IdEntDesc.ToString();
                descuentoEnt.Placeholder = "  Descuento  ";
                descuentoEnt.Keyboard = Keyboard.Numeric;
                descuentoEnt.HorizontalOptions = LayoutOptions.FillAndExpand;
                descuentoEnt.Completed += MyEntryDescuentoSelectedIndexChanged;
                stkF2.Children.Add(descuentoEnt);

                void MyEntryDescuentoSelectedIndexChanged(object sender, EventArgs e)
                {
                    var text_descuento = ((Entry)sender).Text;
                    descArr.SetValue(text_descuento, contador);
                    Descuento = Convert.ToDecimal(text_descuento);
                }

                IdEntCant = IdEntCant + 1;
                Entry cantidadEnt = new Entry();
                cantidadEnt.StyleId = IdEntCant.ToString();
                cantidadEnt.Placeholder = "  Cantidad  ";
                cantidadEnt.Keyboard = Keyboard.Numeric;
                cantidadEnt.HorizontalOptions = LayoutOptions.FillAndExpand;
                cantidadEnt.Completed += MyEntryCantidadSelectedIndexChanged;
                stkF2.Children.Add(cantidadEnt);

                void MyEntryCantidadSelectedIndexChanged(object sender, EventArgs e)
                {
                    var text_cantidad = ((Entry)sender).Text;
                    cantArr.SetValue(text_cantidad, contador);
                    Cantidad = Convert.ToDecimal(text_cantidad);
                    subTotalEnt.Text = ((Convert.ToDecimal(precioEnt.Text) - Convert.ToDecimal(descuentoEnt.Text)) * Convert.ToDecimal(cantidadEnt.Text)).ToString();
                    subtArr.SetValue(subTotalEnt.Text, contador);
                    //aqui puse el metodo resultado xD
                    MontoTotal = 0;
                  
                    resultado();
                    
                }

                Button btnQuitar = new Button();
                btnQuitar.StyleId = IdEntCant.ToString();
                btnQuitar.Text = "   -   ";
                btnQuitar.HorizontalOptions = LayoutOptions.Fill;
                btnQuitar.Clicked += BtnQuitar_Clicked;
                stkF3.Children.Add(btnQuitar);

                contPosicion = contador - 1;
                void BtnQuitar_Clicked(object sender, EventArgs e)
                {
                    decimal subtotalmenos = ((Convert.ToDecimal(precioEnt.Text) - Convert.ToDecimal(descuentoEnt.Text)) * Convert.ToDecimal(cantidadEnt.Text));
                    //MontoTotal = MontoTotal - subtotalmenos;
                    totalVentaEntry.Text = (MontoTotal - subtotalmenos).ToString();
                    cantArr = cantArr.Where(o => o != cantidadEnt.Text).ToArray();
                    subtArr = subtArr.Where(o => o != subTotalEnt.Text).ToArray();
                    stkF0.Children.Remove(StkPrimer);
                    stkF0.Children.Remove(StkSegundo);
                }

                //Entry envaseEnt = new Entry();
                //envaseEnt.Placeholder = "Envase";
                //envaseEnt.Keyboard = Keyboard.Numeric;
                //envaseEnt.WidthRequest = 70;
                ////envaseEnt.Completed += this.MyEntryEnvaseSelectedIndexChanged;
                //stkF1.Children.Add(envaseEnt);
            }
            catch (Exception err)
            {
                await DisplayAlert("ERROR", err.ToString(), "OK", "No se pudo quitar");
            }
        }

        private string vendedorPick;
        private void VendedorPicker_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                vendedorPick = picker.Items[selectedIndex];
            }
        }

        private string clientePick;
        private void ClientePicker_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectIndex = picker.SelectedIndex;
            if (selectIndex != -1)
            {
                clientePick = picker.Items[selectIndex];
            }
        }
        private string tipoVentaPick;
        private void TipoVentaEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                tipoVentaPick = picker.Items[selectedIndex];
                if (selectedIndex == 1)
                {
                    saldo_VentaEntry.IsVisible = true;
                }

                if (selectedIndex == 0)
                {
                    saldo_VentaEntry.IsVisible = false;
                }
            }
        }
        private async void resultado()
        {
            int x = 0;
            for (int r = 0; r < contcampos; r++)
            {
                x = x + 1;
                MontoTotal = MontoTotal + Convert.ToDecimal(subtArr[x]);
            }
            totalVentaEntry.Text = MontoTotal.ToString();
        }

        private async void BtnVentaGuardar_Clicked(object sender, EventArgs e)
        {
            //MontoTotal = MontoTotal + Convert.ToDecimal(subTotalEnt.Text);
            
           

            await DisplayAlert("El moonto total es : ", totalVentaEntry.Text , "OK");
            try
            {
                int y = 0;
                for (int r = 0; r < contcampos; r++)
                {
                    y = y + 1;
                    
                    DetalleVenta detalleVenta = new DetalleVenta()
                    {
                        cantidad = Convert.ToInt32(cantArr[y]),
                        nombre_producto = tiProArr[y] + " " + prodArr[y],
                        precio_producto = Convert.ToDecimal(precioArr[y]),
                        descuento = Convert.ToDecimal(descArr[y]),
                        factura = Convert.ToInt32(numero_facturaVentaEntry.Text)
                    };

                    var json1 = JsonConvert.SerializeObject(detalleVenta);
                    var content1 = new StringContent(json1, Encoding.UTF8, "application/json");
                    HttpClient client1 = new HttpClient();
                    var result1 = await client1.PostAsync("http://dmrbolivia.online/api/ventas/agregarDetalleVenta.php", content1);



                    Datos.Inventario inventario = new Datos.Inventario()
                    {
                        nombre_p = tiProArr[y] + " " + prodArr[y],
                        fecha_inv = fechaVentaEntry.Date,
                        numero_factura = Convert.ToInt32(numero_facturaVentaEntry.Text),
                        detalle = "Venta",
                        precio_compra = 0,
                        unidades = Convert.ToInt32(cantArr[y]),
                        entrada_fisica = 0,
                        salida_fisica = Convert.ToInt32(cantArr[y]),
                        saldo_fisica = Convert.ToDecimal(stockArr[y]) - Convert.ToInt32(cantArr[y]),
                        entrada_valorado = 0,
                        salida_valorado = Convert.ToInt32(cantArr[y]) * Convert.ToDecimal(promedioArr[y]),
                        saldo_valorado = Convert.ToDecimal(stockValoradoArr[y]) - (Convert.ToInt32(cantArr[y]) * Convert.ToDecimal(promedioArr[y])),
                        promedio = (Convert.ToDecimal(stockValoradoArr[y]) / (Convert.ToDecimal(stockArr[y]) ))
                        //promedio = (Convert.ToDecimal(stockValoradoArr[y]) - (Convert.ToInt32(cantArr[y]) * Convert.ToDecimal(promedioArr[y]))) / (Convert.ToDecimal(stockArr[y]) - Convert.ToInt32(cantArr[y])),
                    };

                    var json2 = JsonConvert.SerializeObject(inventario);
                    var content2 = new StringContent(json2, Encoding.UTF8, "application/json");
                    HttpClient client2 = new HttpClient();
                    var result2 = await client2.PostAsync("http://dmrbolivia.online/api/inventarios/agregarInventario.php", content2);

                    Producto producto = new Producto()
                    {
                        id_producto = Convert.ToInt32(idProdArr[y]),                        
                        stock = Convert.ToDecimal(stockArr[y]) - Convert.ToInt32(cantArr[y]),
                        stock_valorado = Convert.ToDecimal(stockValoradoArr[y]) - (Convert.ToInt32(cantArr[y]) * Convert.ToDecimal(promedioArr[y])),
                       
                        promedio = (Convert.ToDecimal(stockValoradoArr[y]) / (Convert.ToDecimal(stockArr[y])))
                    };
                    var json3 = JsonConvert.SerializeObject(producto);
                    var content3 = new StringContent(json3, Encoding.UTF8, "application/json");
                    HttpClient client3 = new HttpClient();
                    var result3 = await client3.PostAsync("http://dmrbolivia.online/api/productos/editarProducto.php", content3);
                    DisplayAlert("Valores", "cantidad= " + cantArr[y] + " | promedio= " + promedioArr[y] + " | stock= " + stockArr[y], "OK");

                }
                Datos.Ventas ventas = new Datos.Ventas()
                {
                    fecha = fechaVentaEntry.Date,
                    numero_factura = Convert.ToInt32(numero_facturaVentaEntry.Text),
                    cliente = clientePick,
                    vendedor = NombreVendedor,
                    tipo_venta = tipoVentaPick,
                    saldo = Convert.ToDecimal(saldo_VentaEntry.Text),
                    total = Convert.ToDecimal(totalVentaEntry.Text)
                };

                var json = JsonConvert.SerializeObject(ventas);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                var result = await client.PostAsync("http://dmrbolivia.online/api/ventas/agregarVenta.php", content);
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    await DisplayAlert("OK", "Se agrego correctamente", "OK");
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

        private void BtnAddProducto_Clicked(object sender, EventArgs e)
        {
            NuevoProducto();
            contcampos = contcampos + 1;
        }
    }
}