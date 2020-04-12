using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessMobil.App.Controls;
using BusinessMobil.App.Controls.Interface;
using BusinessMobil.App.Helpers;
using BusinessMobil.App.Model;
using BusinessMobil.App.Service;
using BusinessMobil.App.Views;
using Xamarin.Forms;

namespace BusinessMobil.App.ViewModel
{
    public class GenerarListaAsistenciaViewModel : BaseViewModel
    {
        Api api = new Api();
        public GenerarListaAsistenciaViewModel()
        {
            IdCompany = 1;
            //Grado = "octavo";
            //Grupo = "A";
            //IdTurno = 1;
            LlenarPiker();
            IsEnable = true;
        }
        public ICommand GenerarListaAsistenciaCommand
        {
            get
            {
                return new Command(GenerarLista);
            }
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
            get=> selectTurnos;
            set=> SetValue(ref selectTurnos,value);
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

        ObservableCollection<ListadoAsistenciaModel> listadoAsistencias;
        public ObservableCollection<ListadoAsistenciaModel> ListadoAsistencias
        {
            get => listadoAsistencias;
            set
            {
                listadoAsistencias = value;
                OnPropertyChanged();
            }
        }

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

        async void GenerarLista()
        {
            if (SelectGrados == null)
            {
                await App.Navigator.DisplayAlert("Validación", "Debe seleccionar el Grado!", "Ok");
                return;
            }

            if (SelectGrupos == null)
            {
                await App.Navigator.DisplayAlert("Validación", "Debe seleccionar el Grupo!", "Ok");
                return;
            }

            if (SelectMateria == null)
            {
                await App.Navigator.DisplayAlert("Validación", "Debe seleccionar la Materia!", "Ok");
                return;
            }

            if (SelectTurnos == null)
            {
                await App.Navigator.DisplayAlert("Validación", "Debe seleccionar el Turno!", "Ok");
                return;
            }

            DependencyService.Get<ILodingPageService>().ShowLoadingPage();
            await Task.Delay(100);
            IsEnable = false;

            var result = await api.GetListRespondeAsync<ListadoAsistenciaModel>($"PersonApi/GetPersonList?idCompany={IdCompany}&grado={SelectGrados.NombreGrado}&grupo={SelectGrupos.NombreGrupo}&idTurno={SelectTurnos.Id}", new Token { access_token = Settings.Token, type_token = Settings.TypeToken });
            if (!result.IsSuccess)
            {
                DependencyService.Get<ILodingPageService>().HideLoadingPage();
                IsEnable = true;
                return;
            }

            try
            {
                Funciones.Funciones f = new Funciones.Funciones();
                var listAsistencia = result.Result as ObservableCollection<ListadoAsistenciaModel>;
                ListadoAsistencias = new ObservableCollection<ListadoAsistenciaModel>
                    (
                    listAsistencia.Select(s => new ListadoAsistenciaModel
                    {
                        Apellido = s.Apellido,
                        Company = s.Company,
                        Date = s.Date,
                        Dni = s.Dni,
                        Email = s.Email,
                        Grado = SelectGrados.NombreGrado,
                        Grupo = SelectGrupos.NombreGrupo,
                        Id = s.Id,
                        IdCompany = s.IdCompany,
                        Matricula = s.Matricula,
                        Nombre = s.Nombre,
                        Rh = s.Rh,
                        Status = s.Status,
                        Turno = s.Turno,
                        Identificador = s.Identificador,
                        Foto = s.Foto,
                        Materia = SelectMateria.NombreMateria,//s.Materia,
                        //ImageSource = f.Base64ToImage(s.Foto)
                    })
                    );

                if(ListadoAsistencias.Count() == 0)
                {
                    DependencyService.Get<ILodingPageService>().HideLoadingPage();
                    IsEnable = true;
                    await Application.Current.MainPage.DisplayAlert("Lista", "No existe ninguna lista de alumnos.", "Ok");
                    return;
                }

                DependencyService.Get<ILodingPageService>().HideLoadingPage();
                await App.Navigator.PushAsync(new ListaAsistenciaPage(listadoAsistencias));
                IsRunning = false;
                IsEnable = true;
            }
            catch (Exception ex)
            {
                IsRunning = false;
                IsEnable = true;
            }
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
            result = await api.GetListRespondeAsync<MateriaClaseModel>($"ToolCompany/GetMaterias?IdCompany={Settings.IdCompany}");

            Materia = result.Result as ObservableCollection<MateriaClaseModel>;

        }
    }
}
