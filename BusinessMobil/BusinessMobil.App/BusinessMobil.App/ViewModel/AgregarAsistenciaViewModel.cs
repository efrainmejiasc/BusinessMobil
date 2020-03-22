using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessMobil.App.Helpers;
using BusinessMobil.App.Model;
using BusinessMobil.App.Service;
using BusinessMobil.App.Views;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace BusinessMobil.App.ViewModel
{
    public class AgregarAsistenciaViewModel : BaseViewModel
    {
        Api api;
        Funciones.Funciones f;
        public AgregarAsistenciaViewModel()
        {
            api = new Api();
            f = new Funciones.Funciones();
            IdCompany = 1;
            LlenarPiker();
            IsEnable = true;
        }

        public ICommand ScanearCodigoCommand => new Command(async () => await ScanCode());

        private int idCompany;
        public int IdCompany
        {
            get { return idCompany; }
            set { idCompany = value; }
        }

        private int idTurno;
        public int IdTurno
        {
            get { return idTurno; }
            set { idTurno = value; }
        }

        ObservableCollection<TurnoModel> turnos;
        public ObservableCollection<TurnoModel> Turnos
        {
            get => turnos;
            set => SetValue(ref turnos, value);
        }

        TurnoModel selectTurnos;
        public TurnoModel SelectTurnos
        {
            get => selectTurnos;
            set => SetValue(ref selectTurnos, value);
        }

        ObservableCollection<GruposModel> grupos;
        public ObservableCollection<GruposModel> Grupos
        {
            get => grupos;
            set => SetValue(ref grupos, value);
        }

        GruposModel selectGrupos;
        public GruposModel SelectGrupos
        {
            get => selectGrupos;
            set => SetValue(ref selectGrupos, value);
        }

        GradosModel selectgrados;
        public GradosModel SelectGrados
        {
            get => selectgrados;
            set => SetValue(ref selectgrados, value);
        }

        ObservableCollection<GradosModel> grados;
        public ObservableCollection<GradosModel> Grados
        {
            get => grados;
            set => SetValue(ref grados, value);
        }

        private string observacion;
        public string Observacion
        {
            get=> observacion;
            set=> SetValue(ref observacion,value);
        }

        async void LlenarPiker()
        {
            var result = await api.GetListRespondeAsync<GruposModel>("PersonApi/GetGrupos");
            if (!result.IsSuccess)
            {
                return;
            }

            Grupos = result.Result as ObservableCollection<GruposModel>;

            result = await api.GetListRespondeAsync<GradosModel>("PersonApi/GetGrados");
            if (!result.IsSuccess)
            {
                return;
            }

            Grados = result.Result as ObservableCollection<GradosModel>;

            Turnos = new ObservableCollection<TurnoModel>()
            {
                new TurnoModel()
                {
                    Id = 1,
                    NombreTurno = "Mañana"
                }
            };
        }
        async Task ScanCode()
        {

            if(SelectGrados == null)
            {
                await App.Navigator.DisplayAlert("Validación","Debe seleccionar el Grado!","Ok");
                return;
            }

            if (SelectGrupos == null)
            {
                await App.Navigator.DisplayAlert("Validación", "Debe seleccionar el Grupo!", "Ok");
                return;
            }

            if (SelectTurnos == null)
            {
                await App.Navigator.DisplayAlert("Validación", "Debe seleccionar el Turno!", "Ok");
                return;
            }

            if(string.IsNullOrEmpty(Observacion))
            {
                await App.Navigator.DisplayAlert("Validación", "Debe introducir la Observación!", "Ok");
                return;
            }

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
                    IsEnable = true;
                    await App.Navigator.PushAsync(new CarnetViewPage(new DatosScanerModel()
                    {
                        IdCompany = 1,
                        Dni = array[2],
                        DniAdm = Settings.DNI,
                        Turno = SelectTurnos,
                        Grado = SelectGrados,
                        Grupo = SelectGrupos,
                        Observacion = Observacion
                    }));
                });
            };
        }
    }
}
