using BusinessMobil.App.Helpers;
using BusinessMobil.App.Model;
using BusinessMobil.App.Service;
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
            //api = new Api();
            //IdCompany = 1;
            //Grado = "octavo";
            //Grupo = "A";
            //IdTurno = 1;
            //Task.FromResult(GetListadoAsistenciaAsync());
            //SelectAsistenciaCommand = new Command(async () => await GetSelectAsistencia());
            ListadoAsistencias = _listadoAsistencias;
        }

        public ICommand SelectAsistenciaCommand { get; set; }

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
                var asistencia = ListadoAsistencias.FirstOrDefault(f => f.Id == SelectItem.Id);
                asistencia.SelectAsistencia = true;
            }
        }
        async Task GetListadoAsistenciaAsync()
        {
            var result = await api.GetListRespondeAsync<ListadoAsistenciaModel>($"PersonApi/GetPersonList?idCompany={IdCompany}&grado={Grado}&grupo={Grupo}&idTurno={IdTurno}", new Token { access_token = Settings.Token, type_token = Settings.TypeToken});
            if (!result.IsSuccess)
            {
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
            }
            catch (Exception ex)
            {

            }
        }
    }
}
