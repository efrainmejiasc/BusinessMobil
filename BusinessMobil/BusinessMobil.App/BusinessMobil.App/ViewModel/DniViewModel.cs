using BusinessMobil.App.Controls.Interface;
using BusinessMobil.App.Helpers;
using BusinessMobil.App.Model;
using BusinessMobil.App.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BusinessMobil.App.ViewModel
{
    public class DniViewModel : BaseViewModel
    {
        string ruta = "http://joselelu-001-site2.etempurl.com/digitalcard/";
        public ICommand AgregarAsistenciaCommand => new Command(async () => await GetAsistencia());
        public ICommand BuscarDatosCommand => new Command(async () => await GetDniAsync());

        private DatosScanerModel datosScaner;
        public DatosScanerModel DatosScaner
        {
            get => datosScaner;
            set => SetValue(ref datosScaner, value);
        }

        private ImageSource _carnet;
        public ImageSource Carnet
        {
            get => _carnet;
            set => SetValue(ref _carnet, value);
        }

        private string dni;
        public string Dni
        {
            get => dni;
            set => SetValue(ref dni, value);
        }

        async Task GetDniAsync()
        {
            DependencyService.Get<ILodingPageService>().ShowLoadingPage();
            Uri uri = new Uri($"{ruta}{Dni}.jpg");
            DatosScaner.Dni = Dni;
            DatosScaner.DniAdm = Settings.DNI;
            DatosScaner.IdCompany = Settings.IdCompany;

            Carnet = new UriImageSource
            {
                Uri = uri,
                CachingEnabled = false
            };
            await Task.Delay(3000);
            DependencyService.Get<ILodingPageService>().HideLoadingPage();
        }
        async Task GetAsistencia()
        {
            if (string.IsNullOrEmpty(DatosScaner.Dni))
            {
                await App.Navigator.DisplayAlert("Campo obligatorio", "Debe ingresar el Documento de Identidad", "Ok");
                return;
            }
            await App.Navigator.PushAsync(new AgregarAsistenciaPage(DatosScaner));
        }
    }
}
