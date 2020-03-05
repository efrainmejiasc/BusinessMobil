using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessMobil.App.Model;
using BusinessMobil.App.Views;
using Xamarin.Forms;

namespace BusinessMobil.App.ViewModel
{
    public class MenuItemViewModel : MenuModel
    {
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
                    await App.Navigator.PushAsync(new ScannerPage());
                    break;
                case "RegisterDevicePage":
                    await App.Navigator.PushAsync(new RegisterDevicePage());
                    break;
                case "ListaAsistenciaPage":
                    await App.Navigator.PushAsync(new ListaAsistenciaPage());
                    break;
                default:
                    break;
            }
        }

    }
}
