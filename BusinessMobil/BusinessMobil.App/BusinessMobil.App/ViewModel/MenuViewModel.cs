using BusinessMobil.App.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BusinessMobil.App.ViewModel
{
    public class MenuViewModel
    {
        public INavigation Navigation { get; set; }

        public ICommand ScannerCommand { get; set; }
        public ICommand RegisterDeviceCommand { get; set; }

        public MenuViewModel(INavigation navigation)
        {
            Navigation = navigation;
            ScannerCommand = new Command(async () => await GoToScannerView());
            RegisterDeviceCommand = new Command(async () => await RegisterDevice());
        }

        private async Task GoToScannerView() => await Navigation.PushAsync(new ScannerPage());
        private async Task RegisterDevice() => await Navigation.PushAsync(new RegisterDevicePage());
    }
}
