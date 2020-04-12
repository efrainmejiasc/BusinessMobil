using BusinessMobil.App.Controls.Interface;
using BusinessMobil.App.Helpers;
using BusinessMobil.App.Model;
using BusinessMobil.App.Service;
using BusinessMobil.App.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BusinessMobil.App.ViewModel
{
    public class ListaAsistenciaViewModel : BaseViewModel
    {
        Api api;
        public ListaAsistenciaViewModel(ObservableCollection<ListadoAsistenciaModel> _listadoAsistencias)
        {
            api = new Api();
            //IdCompany = 1;
            //Grado = "octavo";
            //Grupo = "A";
            //IdTurno = 1;
            //Task.FromResult(GetListadoAsistenciaAsync());
            GenerarAsistenciaCommand = new Command(async () => await GenerarAsistencia());
            ListadoAsistencias = _listadoAsistencias;
        }

        public ICommand GenerarAsistenciaCommand { get; set; }

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

        private ListadoAsistenciaModel _SelectItem;
        public ListadoAsistenciaModel SelectItem
        {
            get => _SelectItem;
            set
            {
                SetValue(ref _SelectItem, value);
                if (_SelectItem != null)
                    GetSelectAsistencia();
            }
        }

        private void GetSelectAsistencia()
        {
            if (SelectItem != null)
            {
                var tempLista = new ObservableCollection<ListadoAsistenciaModel>();
                tempLista = ListadoAsistencias;
                var asistencia = tempLista.FirstOrDefault(f => f.Id == SelectItem.Id);
                asistencia.Status = asistencia.Status == false ? true : false;
                ListadoAsistencias = null;
                ListadoAsistencias = tempLista;
            }
        }

        async Task GenerarAsistencia()
        {
            DependencyService.Get<ILodingPageService>().ShowLoadingPage();
            List<AsistenciaClaseModel> listAsist = new List<AsistenciaClaseModel>();
            listAsist = new List<AsistenciaClaseModel>
                    (
                        ListadoAsistencias.Select(s => new AsistenciaClaseModel
                        {
                            Dni = s.Dni,
                            EmailSend = s.Email,
                            Id = s.Id,
                            IdCompany = s.IdCompany,
                            Status = s.Status,
                            Turno = s.Turno,
                            DniAdm = Settings.DNI,
                            Materia = s.Materia,
                            Grupo = s.Grupo,
                            Grado = s.Grado,
                            CreateDate = DateTime.Now
                        })
                    );
            try
            {
                var result = await api.PostListRespondeAsync("AsistenciaClaseApi/AsistenciaClase", listAsist, new Token { access_token = Settings.Token, type_token = Settings.TypeToken });
                if (!result.IsSuccess)
                {
                    DependencyService.Get<ILodingPageService>().HideLoadingPage();
                    await Application.Current.MainPage.DisplayAlert("Error!", result.Message, "Ok");
                    return;
                }
                DependencyService.Get<ILodingPageService>().HideLoadingPage();
                await Application.Current.MainPage.DisplayAlert("Listado de Asistencia", "Se ha enviado con exito!", "Ok");
                //await App.Navigator.PushAsync(new HomePage());
                Application.Current.MainPage = new MasterPage();
            }
            catch (Exception ex)
            {
                DependencyService.Get<ILodingPageService>().HideLoadingPage();
                await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok");
            }
        }
    }
}
