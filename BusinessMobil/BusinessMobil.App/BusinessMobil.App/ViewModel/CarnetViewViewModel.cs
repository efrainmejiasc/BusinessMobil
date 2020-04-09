using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessMobil.App.Controls.Interface;
using BusinessMobil.App.Helpers;
using BusinessMobil.App.Model;
using BusinessMobil.App.Service;
using BusinessMobil.App.Views;
using Xamarin.Forms;

namespace BusinessMobil.App.ViewModel
{
    public class CarnetViewViewModel : BaseViewModel
    {
        Api api;
        string ruta = "http://joselelu-001-site2.etempurl.com/digitalcard/";

        public CarnetViewViewModel(DatosScanerModel carnet)
        {
            api = new Api();
            Uri uri = new Uri($"{ruta}{carnet.Dni}.jpg");
            DatosScaner = carnet;
            Carnet = new UriImageSource
            {
                Uri = uri,
                CachingEnabled = false
            };
            Task.Delay(3000);
        }

        public ICommand EnviarCommand => new Command(async () => await EnviarAsistencia());
        public ICommand AgregarAsistenciaCommand => new Command(async () => await App.Navigator.PushAsync(new AgregarAsistenciaPage(DatosScaner)));

        private DatosScanerModel datosScaner;
        public DatosScanerModel DatosScaner
        {
            get=> datosScaner;
            set => SetValue(ref datosScaner,value);
        }

        private ImageSource _carnet;
        public ImageSource Carnet
        {
            get => _carnet;
            set => SetValue(ref _carnet, value);
        }

        async Task EnviarAsistencia()
        {
            DependencyService.Get<ILodingPageService>().ShowLoadingPage();
            var obs = new ObservasionClaseModel()
            {
                Dni = DatosScaner.Dni,
                IdCompany = DatosScaner.IdCompany,
                Status = true,
                CreateDate = DateTime.Now,
                DniAdm = DatosScaner.DniAdm,
                Observacion = DatosScaner.Observacion,
                Materia = DatosScaner.Materia.Materia,
            };
            var result = await api.PostRespondeAsync("AsistenciaClaseApi/ObservacionClase", obs, new Token { access_token = Settings.Token, type_token = Settings.TypeToken });
            if(!result.IsSuccess)
            {
                DependencyService.Get<ILodingPageService>().HideLoadingPage();
                return;
            }

            DependencyService.Get<ILodingPageService>().HideLoadingPage();
            await App.Navigator.PushAsync(new HomePage());
        }
    }
}
