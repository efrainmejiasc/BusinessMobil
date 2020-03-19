using System;
using System.Threading;
using System.Threading.Tasks;
using BusinessMobil.App.Controls.Interface;
using Xamarin.Forms;

namespace BusinessMobil.App.ViewModel
{
    public class CarnetViewViewModel : BaseViewModel
    {
        string ruta = "http://joselelu-001-site2.etempurl.com/digitalcard/";

        public CarnetViewViewModel(string carnet)
        {
            IsRunning = true;
            Uri uri = new Uri($"{ ruta }{ carnet}.jpg");
            Carnet = new UriImageSource
            {
                Uri = uri,
                CachingEnabled = false
            };
            Thread.Sleep(4000);
            IsRunning = false;
        }

        private ImageSource _carnet;
        public ImageSource Carnet
        {
            get => _carnet;
            set => SetValue(ref _carnet, value);
        }
    }
}
