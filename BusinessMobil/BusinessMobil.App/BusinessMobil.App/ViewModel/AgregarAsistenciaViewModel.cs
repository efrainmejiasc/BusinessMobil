using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessMobil.App.Controls.Interface;
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
        public AgregarAsistenciaViewModel(DatosScanerModel datos)
        {
            api = new Api();
            f = new Funciones.Funciones();
            IdCompany = 1;
            DatosScaner = datos;
            //Task.Run(async () =>
            //{
            //    await LlenarPiker();
            //    await GetDatosAlumno();
            //});
            Task.Run(async ()=> await LlenarPiker());
            Task.Run(async ()=> await GetDatosAlumno());
            IsEnable = true;
        }

        //public ICommand ScanearCodigoCommand => new Command(async () => await ScanCode());
        public ICommand EnviarCommand => new Command(async () => await EnviarAsistencia());

        #region Propiedades
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

        ObservableCollection<MateriaClaseModel> materia;
        public ObservableCollection<MateriaClaseModel> Materia
        {
            get => materia;
            set => SetValue(ref materia, value);
        }

        MateriaClaseModel selectMateria;
        public MateriaClaseModel SelectMateria
        {
            get => selectMateria;
            set => SetValue(ref selectMateria, value);
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

        bool status;
        public bool Status
        {
            get => status;
            set=> SetValue(ref status,value);
        }

        private string observacion;
        public string Observacion
        {
            get => observacion;
            set => SetValue(ref observacion, value);
        }

        ObservableCollection<ListadoAsistenciaModel> listadoAsistencias;
        public ObservableCollection<ListadoAsistenciaModel> ListadoAsistencias
        {
            get => listadoAsistencias;
            private set => SetValue(ref listadoAsistencias, value);
        }

        private DatosScanerModel datosScaner;
        public DatosScanerModel DatosScaner
        {
            get => datosScaner;
            set => SetValue(ref datosScaner, value);
        }

        private string _Grado;
        public string Grado
        {
            get { return _Grado; }
            set => SetValue(ref _Grado,value);
        }

        private string _Grupo;
        public string Grupo
        {
            get { return _Grupo; }
            set => SetValue(ref _Grupo, value);
        }

        private string _Turno;
        public string Turno
        {
            get { return _Turno; }
            set => SetValue(ref _Turno,value);
        }

        private ListadoAsistenciaModel _alumno;
        public ListadoAsistenciaModel Alumno
        {
            get { return _alumno; }
            set => SetValue(ref _alumno, value);
        }

        private int _IdAsistencia;

        public int IdAsistencia
        {
            get { return _IdAsistencia; }
            set => SetValue(ref _IdAsistencia, value);
        }


        #endregion

        async Task LlenarPiker()
        {
            DependencyService.Get<ILodingPageService>().ShowLoadingPage();
            var result = await api.GetListRespondeAsync<GruposModel>($"PersonApi/GetGrupos?idCompany={IdCompany}");
            if (!result.IsSuccess)
            {
                return;
            }

            Grupos = result.Result as ObservableCollection<GruposModel>;

            result = await api.GetListRespondeAsync<GradosModel>($"PersonApi/GetGrados?idCompany={IdCompany}");
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

            result = await api.GetListRespondeAsync<MateriaClaseModel>($"ToolCompany/GetMaterias?IdCompany={Settings.IdCompany}");
            if (!result.IsSuccess)
            {
                return;
            }

            Materia = result.Result as ObservableCollection<MateriaClaseModel>;

            DependencyService.Get<ILodingPageService>().HideLoadingPage();
        }
        //async Task ScanCode()
        //{

        //    if (SelectGrados == null)
        //    {
        //        await App.Navigator.DisplayAlert("Validación", "Debe seleccionar el Grado!", "Ok");
        //        return;
        //    }

        //    if (SelectGrupos == null)
        //    {
        //        await App.Navigator.DisplayAlert("Validación", "Debe seleccionar el Grupo!", "Ok");
        //        return;
        //    }

        //    if (SelectMateria == null)
        //    {
        //        await App.Navigator.DisplayAlert("Validación", "Debe seleccionar la Materia!", "Ok");
        //        return;
        //    }

        //    if (SelectTurnos == null)
        //    {
        //        await App.Navigator.DisplayAlert("Validación", "Debe seleccionar el Turno!", "Ok");
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(Observacion))
        //    {
        //        await App.Navigator.DisplayAlert("Validación", "Debe introducir la Observación!", "Ok");
        //        return;
        //    }

        //    var options = new MobileBarcodeScanningOptions();

        //    options.PossibleFormats = new List<BarcodeFormat>()
        //    {
        //        ZXing.BarcodeFormat.EAN_8,
        //        ZXing.BarcodeFormat.EAN_13,
        //        ZXing.BarcodeFormat.AZTEC,
        //        ZXing.BarcodeFormat.QR_CODE
        //    };

        //    var overlay = new ZXingDefaultOverlay
        //    {
        //        ShowFlashButton = false,
        //        TopText = "Coloca el código QR frente al dispositivo",
        //        BottomText = "El escaneo es automático",
        //        Opacity = 0.75
        //    };
        //    overlay.BindingContext = overlay;

        //    var page = new ZXingScannerPage(options, overlay)
        //    {
        //        Title = "Escanear QR",
        //        DefaultOverlayShowFlashButton = true,
        //    };
        //    await App.Navigator.PushAsync(page);

        //    page.OnScanResult += (result) =>
        //    {
        //        page.IsScanning = false;

        //        Device.BeginInvokeOnMainThread(async () =>
        //        {
        //            await GetExisteAlumno();
        //            string[] array = f.base64Decode(result.Text).Split('#');
        //            var dni = array[2];
        //            IsEnable = true;
        //            var exist = ListadoAsistencias.Any(a => a.Dni == dni);
        //            if (!ListadoAsistencias.Any(a => a.Dni == dni))
        //            {
        //                await App.Navigator.DisplayAlert("Alerta", $"Este estudiante en el Grado: {SelectGrados.NombreGrado}, Grupo: {SelectGrupos.NombreGrupo}, Materia: {SelectMateria.Materia}, Turno: {SelectTurnos.NombreTurno} !", "Ok");
        //                await App.Navigator.PushAsync(new HomePage());
        //                return;
        //            }

        //            await App.Navigator.PushAsync(new CarnetViewPage(new DatosScanerModel()
        //            {
        //                IdCompany = 1,
        //                Dni = array[2],
        //                DniAdm = Settings.DNI,
        //                Turno = SelectTurnos,
        //                Grado = SelectGrados,
        //                Grupo = SelectGrupos,
        //                Materia = SelectMateria,
        //                Observacion = Observacion
        //            }));
        //        });
        //    };
        //}
        async Task GetDatosAlumno()
        {
            try
            {
                DependencyService.Get<ILodingPageService>().ShowLoadingPage();
                await Task.Delay(400);
                var result = await api.GetrespondeAsync<ListadoAsistenciaModel>($"PersonApi/GetPerson?identificador={DatosScaner.Base64Dni}", new Token { access_token = Settings.Token, type_token = Settings.TypeToken });
                if (!result.IsSuccess)
                {
                    DependencyService.Get<ILodingPageService>().HideLoadingPage();
                    IsEnable = true;
                    return;
                }

                Alumno = result.Result as ListadoAsistenciaModel;

                Turno = Alumno.Turno.ToString();
                SelectTurnos = Turnos.FirstOrDefault(f => f.Id == Alumno.Turno);
                Grupo = Alumno.Grupo;
                Grado = Alumno.Grado;

                DependencyService.Get<ILodingPageService>().HideLoadingPage();
            }
            catch (Exception ex)
            {
                DependencyService.Get<ILodingPageService>().HideLoadingPage();
            }
        }
        //async Task GetExisteAlumno()
        //{
        //    var result = await api.GetListRespondeAsync<ListadoAsistenciaModel>($"PersonApi/GetPersonList?idCompany={IdCompany}&grado={SelectGrados.NombreGrado}&grupo={SelectGrupos.NombreGrupo}&idTurno={SelectTurnos.Id}", new Token { access_token = Settings.Token, type_token = Settings.TypeToken });
        //    if (!result.IsSuccess)
        //    {
        //        DependencyService.Get<ILodingPageService>().HideLoadingPage();
        //        IsEnable = true;
        //        return;
        //    }

        //    try
        //    {
        //        Funciones.Funciones f = new Funciones.Funciones();
        //        var listAsistencia = result.Result as ObservableCollection<ListadoAsistenciaModel>;

        //        if (listAsistencia.Count == 0)
        //        {
        //            DependencyService.Get<ILodingPageService>().HideLoadingPage();
        //            IsEnable = true;
        //            await Application.Current.MainPage.DisplayAlert("Lista", "No existe ninguna lista de alumnos.", "Ok");
        //            return;
        //        }

        //        ListadoAsistencias = new ObservableCollection<ListadoAsistenciaModel>
        //            (
        //            listAsistencia.Select(s => new ListadoAsistenciaModel
        //            {
        //                Apellido = s.Apellido,
        //                Company = s.Company,
        //                Date = s.Date,
        //                Dni = s.Dni,
        //                Email = s.Email,
        //                Grado = s.Grado,
        //                Grupo = s.Grupo,
        //                Id = s.Id,
        //                IdCompany = s.IdCompany,
        //                Matricula = s.Matricula,
        //                Nombre = s.Nombre,
        //                Rh = s.Rh,
        //                Status = s.Status,
        //                Turno = s.Turno,
        //                Identificador = s.Identificador,
        //                Foto = s.Foto,
        //                Materia = "Matematica"//s.Materia,
        //                //ImageSource = f.Base64ToImage(s.Foto)
        //            })
        //            );

        //        //DependencyService.Get<ILodingPageService>().HideLoadingPage();
        //    }
        //    catch (Exception ex)
        //    {
        //        IsEnable = true;
        //    }
        //}
        async Task EnviarAsistencia()
        {
            //if (SelectGrados == null)
            //{
            //    await App.Navigator.DisplayAlert("Validación", "Debe seleccionar el Grado!", "Ok");
            //    return;
            //}

            //if (SelectGrupos == null)
            //{
            //    await App.Navigator.DisplayAlert("Validación", "Debe seleccionar el Grupo!", "Ok");
            //    return;
            //}

            if (SelectMateria == null)
            {
                await App.Navigator.DisplayAlert("Validación", "Debe seleccionar la Materia!", "Ok");
                return;
            }

            //if (SelectTurnos == null)
            //{
            //    await App.Navigator.DisplayAlert("Validación", "Debe seleccionar el Turno!", "Ok");
            //    return;
            //}

            if (string.IsNullOrEmpty(Observacion))
            {
                await App.Navigator.DisplayAlert("Validación", "Debe introducir la Observación!", "Ok");
                return;
            }

            DependencyService.Get<ILodingPageService>().ShowLoadingPage();
            await Task.Delay(300);

            var result = await api.GetrespondeAsync<AsistenciaModel>($"AsistenciaClaseApi/GetAsistenciaClaseEstudiante?fecha={DateTime.Now.Date}&dni={Alumno.Dni}&materia={SelectMateria.NombreMateria}&grado={Grado}&grupo={Grupo}&idCompany={IdCompany}", new Token { access_token = Settings.Token, type_token = Settings.TypeToken });
            if (!result.IsSuccess)
            {
                DependencyService.Get<ILodingPageService>().HideLoadingPage();
                IsEnable = true;
                return;
            }
            var Asist = result.Result as AsistenciaModel;
            IdAsistencia = Asist.Id;

            var obs = new ObservasionClaseModel()
            {
                Dni = DatosScaner.Dni,
                IdCompany = DatosScaner.IdCompany,
                Status = Status,
                CreateDate = DateTime.Now,
                DniAdm = DatosScaner.DniAdm,
                Observacion = Observacion,
                Materia = SelectMateria.NombreMateria,
                IdAsistencia = IdAsistencia
            };

            result = await api.PostRespondeAsync("AsistenciaClaseApi/ObservacionClase", obs, new Token { access_token = Settings.Token, type_token = Settings.TypeToken });
            if (!result.IsSuccess)
            {
                DependencyService.Get<ILodingPageService>().HideLoadingPage();
                return;
            }

            DependencyService.Get<ILodingPageService>().HideLoadingPage();
            //await App.Navigator.PushAsync(new HomePage());
            Application.Current.MainPage = new MasterPage();
        }
    }
}
