using BusinessMobil.App.Helpers;
using BusinessMobil.App.Model;
using BusinessMobil.App.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace BusinessMobil.App.ViewModel
{
    public class ListaAsistenciaViewModel : BaseViewModel
    {
        Api api;
        public ListaAsistenciaViewModel()
        {
            api = new Api();
            IdCompany = 1;
            Grado = "octavo";
            Grupo = "A";
            IdTurno = 1;
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


        async Task GetListadoAsistenciaAsync()
        {
            var result = await api.GetListRespondeAsync<ListadoAsistenciaModel>($"PersonApi/GetPersonList?idCompany={IdCompany}&grado={Grado}&grupo={Grupo}&idTurno={IdTurno}", new Token { access_token = Settings.Token, type_token = Settings.TypeToken});
            if (!result.IsSuccess)
            {
                return;
            }

            var listAsistencia = result.Result as ObservableCollection<ListadoAsistenciaModel>;

        }
    }
}
