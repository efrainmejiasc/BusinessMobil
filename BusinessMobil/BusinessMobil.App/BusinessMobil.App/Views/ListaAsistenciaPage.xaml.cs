using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BusinessMobil.App.Model;
using BusinessMobil.App.ViewModel;
using Xamarin.Forms;

namespace BusinessMobil.App.Views
{
    public partial class ListaAsistenciaPage : ContentPage
    {
        public ListaAsistenciaPage(ObservableCollection<ListadoAsistenciaModel> listadoAsistencias)
        {
            InitializeComponent();
            BindingContext = new ListaAsistenciaViewModel(listadoAsistencias);
        }
    }
}
