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
            Grado = "octavo";
            Grupo = "A";
            IdTurno = 1;
            IsRunning = false;
            IsEnable = true;

            Grupos.Add("A");
            Grados.Add("Octavo");
            Turnos.Add("Mañana");

        }
        public ICommand GenerarListaAsistenciaCommand
        {
            get
            {               
                return new Command(GenerarLista);
            }
        }

        ObservableCollection<string> grupos;
        public ObservableCollection<string> Grupos
        {
            get => grupos;
            set => SetValue(ref grupos, value);
        }

        ObservableCollection<string> grados;
        public ObservableCollection<string> Grados
        {
            get => grados;
            set => SetValue(ref grados, value);
        }

        ObservableCollection<string> turnos;
        public ObservableCollection<string> Turnos
        {
            get => turnos;
            set => SetValue(ref turnos, value);
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

        private string grado;
        public string Grado
        {
            get { return grado; }
            set { grado = value; }
        }

        private string grupo;
        public string Grupo
        {
            get { return grupo; }
            set { grupo = value; }
        }

        private int idTurno;
        public int IdTurno
        {
            get { return idTurno; }
            set { idTurno = value; }
        }
        async void GenerarLista()
        {
            //await Application.Current.MainPage.DisplayAlert("", "Prueba", "Ok");
            //DependencyService.Get<ILodingPageService>().ShowLoadingPage();
            
            DependencyService.Get<ILodingPageService>().ShowLoadingPage();
            await Task.Delay(100);

            IsRunning = true;
            IsEnable = false;
            var result = await api.GetListRespondeAsync<ListadoAsistenciaModel>($"PersonApi/GetPersonList?idCompany={IdCompany}&grado={Grado}&grupo={Grupo}&idTurno={IdTurno}", new Token { access_token = Settings.Token, type_token = Settings.TypeToken });
            if (!result.IsSuccess)
            {
                IsRunning = false;
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
                        Grado = s.Grado,
                        Grupo = s.Grupo,
                        Id = s.Id,
                        IdCompany = s.IdCompany,
                        Matricula = s.Matricula,
                        Nombre = s.Nombre,
                        Rh = s.Rh,
                        Status = s.Status,
                        Turno = s.Turno,
                        ImageSource = f.Base64ToImage(s.Foto)
                    })
                    );

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
    }
}
