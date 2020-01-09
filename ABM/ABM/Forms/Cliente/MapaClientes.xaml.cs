using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using Xamarin.Essentials;
using System.Threading;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;

namespace ABM.Forms.Cliente
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapaClientes : ContentPage
	{
        private string Longitud;
        private string Latitud;
        private string Longitud2;
        private string Latitud2;
        private string Longitud3;
        private string Latitud3;
        private string Nombre;
        private string Nombre2;
        private string Nombre3;
        public MapaClientes ()
		{
            try
            {
                InitializeComponent ();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }

            MostrarMapaAsync();
            Device.BeginInvokeOnMainThread(() => AskPermission());
            //CheckPermissionStatusRepeatedly();
        }
        
        //private void Street_OnClicked(object sender, EventArgs e)
        //{
        //    MapView.MapType = MapType.Street;
        //}


        //private void Hybrid_OnClicked(object sender, EventArgs e)
        //{
        //    MapView.MapType = MapType.Hybrid;
        //}

        //private void Satellite_OnClicked(object sender, EventArgs e)
        //{
        //    MapView.MapType = MapType.Satellite;
        //}

        private async void MostrarMapaAsync()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    try
                    {
                        HttpClient client = new HttpClient();
                        var response = await client.GetStringAsync("http://dmrbolivia.online/api/clientes/listaCliente.php");
                        var clientes = JsonConvert.DeserializeObject<List<Datos.Cliente>>(response);

                        var location = await Geolocation.GetLastKnownLocationAsync();

                        var map = new Xamarin.Forms.Maps.Map(MapSpan.FromCenterAndRadius(
                        new Position(location.Latitude, location.Longitude),
                        Distance.FromKilometers(8)))
                        {
                            IsShowingUser = true,
                            VerticalOptions = LayoutOptions.FillAndExpand,
                        };
                        
                        foreach (var item in clientes)
                        {
                            double latitud = Convert.ToDouble(item.ubicacion_latitud);
                            double longitud = Convert.ToDouble(item.ubicacion_longitud);
                            var position = new Position(latitud, longitud);

                            var pin = new Pin
                            {
                                Type = PinType.Place,
                                Position = position,
                                Label = item.nombre,
                                Address = item.telefono.ToString(),
                            };

                            map.Pins.Add(pin);
                        }

                        //var position1 = new Position(-17.49616, -63.68657);
                        //var position2 = new Position(-17.10181, -63.85874);
                        //var position3 = new Position(-17.10182, -63.36521);
                        //var position4 = new Position(-17.89320, -63.48363);
                        //var position5 = new Position(-17.89741, -63.52544);
                        //var position6 = new Position(-17.89262, -63.68735);

                        //var pin1 = new Pin
                        //{
                        //    Type = PinType.Place,
                        //    Position = position1,
                        //    Label = "IntilaQ",
                        //    Address = "www.intilaq.tn",
                        //};

                        //var pin2 = new Pin
                        //{
                        //    Type = PinType.Place,
                        //    Position = position2,
                        //    Label = "Telnet R&D",
                        //    Address = "www.groupe-telnet.com"
                        //};

                        //var pin3 = new Pin
                        //{
                        //    Type = PinType.Place,
                        //    Position = position3,
                        //    Label = "Telnet R&D",
                        //    Address = "www.kromberg-schubert.com"
                        //};

                        //var pin4 = new Pin
                        //{
                        //    Type = PinType.Place,
                        //    Position = position4,
                        //    Label = "Telnet R&D",
                        //    Address = "www.kromberg-schubert.com"
                        //};

                        //var pin5 = new Pin
                        //{
                        //    Type = PinType.Place,
                        //    Position = position5,
                        //    Label = "Telnet R&D",
                        //    Address = "www.kromberg-schubert.com"
                        //};

                        //var pin6 = new Pin
                        //{
                        //    Type = PinType.Place,
                        //    Position = position6,
                        //    Label = "Telnet R&D",
                        //    Address = "www.kromberg-schubert.com"
                        //};

                        //map.Pins.Add(pin1);
                        //map.Pins.Add(pin2);
                        //map.Pins.Add(pin3);
                        //map.Pins.Add(pin4);
                        //map.Pins.Add(pin5);
                        //map.Pins.Add(pin6);

                        Content = map;
                    }

                    catch (Exception err)
                    {
                        await DisplayAlert("Location Denied", err.ToString(), "OK");
                    }
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Location Denied", ex.ToString(), "OK");
            }


            
                
        }

        async void AskPermission()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (status != PermissionStatus.Granted)
            {
                await Application.Current.MainPage.DisplayAlert("Permission Request", "This app needs to access device location. Please allow access for location.", "Ok");
                try
                {
                    await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
                }
                catch (Exception ex)
                {
                    await DisplayAlert("ERROR", ex.ToString(), "OK");
                    //Location.Text = "Error: " + ex;
                }
            }
        }

        //void CheckPermissionStatusRepeatedly()
        //{
        //    Timer timer = null;
        //    timer = new Timer(new TimerCallback(async delegate
        //    {
        //        var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
        //        if (status != PermissionStatus.Granted)
        //            Debug.WriteLine("Still permission is not Granted");
        //        else
        //        {
        //            Debug.WriteLine("Now permision is Granted, Hence calling GetLocation()");
        //            Device.BeginInvokeOnMainThread(() => GetLocation());
        //            timer.Dispose();
        //        }
        //    }), "test", 1000, 1000);
        //}

        //async void GetLocation()
        //{
        //    Location location;
        //    location = await Geolocation.GetLastKnownLocationAsync();
        //    //Location.Text = "Lat: " + location.Latitude + " Long: " + location.Longitude;
        //}
    }
}