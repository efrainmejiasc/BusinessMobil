using System;
using System.Windows.Input;
using BusinessMobil.App.Views;
using Xamarin.Forms;

namespace BusinessMobil.App.ViewModel
{
    public class MenuScanerViewModel
    {
        public ICommand AgregarAsistenciaCommand => new Command(async () => await App.Navigator.PushAsync(new AgregarAsistenciaPage()));
    }
}
