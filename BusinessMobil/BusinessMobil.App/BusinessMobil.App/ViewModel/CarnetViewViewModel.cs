using System;
namespace BusinessMobil.App.ViewModel
{
    public class CarnetViewViewModel : BaseViewModel
    {
        string ruta = "http://joselelu-001-site2.etempurl.com/digitalcard/";

        public CarnetViewViewModel(string carnet)
        {
            Carnet = carnet;
        }

        private string _carnet;
        public string Carnet
        {
            get => _carnet;
            set => SetValue(ref _carnet, value);
        }
    }
}
