using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessMobil.App.Helpers;
using BusinessMobil.App.Model;
using BusinessMobil.App.Views;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace BusinessMobil.App.ViewModel
{
    public class MenuItemViewModel : MenuModel
    {
        Funciones.Funciones f = new Funciones.Funciones();

        public MenuItemViewModel()
        {

        }

        public ICommand SelectMenuCommand => new Command(async () => await SelectMenu());

        async Task SelectMenu()
        {
            App.Master.IsPresented = false;

            switch (PageName)
            {
                case "ScannerPage":
                    await ScanCode();
                    break;
                case "RegisterDevicePage":
                    await App.Navigator.PushAsync(new RegisterDevicePage());
                    break;
                case "GenerarListaAsistenciaPage":
                    await App.Navigator.PushAsync(new GenerarListaAsistenciaPage());
                    break;
                case "PerfilPage":
                    await App.Navigator.PushAsync(new PerfilPage());
                    break;
                case "CarnetPage":
                    await App.Navigator.PushAsync(new DniPage());
                    break;
                case "SalirPage":
                    Settings.Email = string.Empty;
                    Settings.IdCompany = 0;
                    Settings.IdTypeUser = 0;
                    Settings.Password = string.Empty;
                    Settings.Status = false;
                    Settings.Token = string.Empty;
                    Settings.TypeToken = string.Empty;
                    Settings.DNI = string.Empty;
                    Settings.User = string.Empty;
                    Settings.Id = string.Empty;
                    await App.Navigator.PushAsync(new Login());
                    break;
                default:
                    break;
            }
        }
        async Task ScanCode()
        {
            var options = new MobileBarcodeScanningOptions();

            options.PossibleFormats = new List<BarcodeFormat>()
            {
                ZXing.BarcodeFormat.EAN_8,
                ZXing.BarcodeFormat.EAN_13,
                ZXing.BarcodeFormat.AZTEC,
                ZXing.BarcodeFormat.QR_CODE
            };

            var overlay = new ZXingDefaultOverlay
            {
                ShowFlashButton = false,
                TopText = "Coloca el código QR frente al dispositivo",
                BottomText = "El escaneo es automático",
                Opacity = 0.75
            };
            overlay.BindingContext = overlay;

            var page = new ZXingScannerPage(options, overlay)
            {
                Title = "Escanear QR",
                DefaultOverlayShowFlashButton = true,
            };
            await App.Navigator.PushAsync(page);

            page.OnScanResult += (result) =>
            {
                page.IsScanning = false;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    string[] array = f.base64Decode(result.Text).Split('#');
                    await App.Navigator.PushAsync(new CarnetViewPage(new DatosScanerModel() 
                    { 
                        Base64Dni = result.Text ,
                        IdCompany = 1,
                        Dni = array[2],
                        DniAdm = Settings.DNI,
                    }));
                });
            };
        }
    }
}
