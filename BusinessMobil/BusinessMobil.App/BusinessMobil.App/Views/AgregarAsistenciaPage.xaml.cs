using BusinessMobil.App.Model;
using BusinessMobil.App.ViewModel;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BusinessMobil.App.Views
{
    public partial class AgregarAsistenciaPage : ContentPage
    {
        public AgregarAsistenciaPage(DatosScanerModel datos)
        {
            InitializeComponent();
            BindingContext = new AgregarAsistenciaViewModel(datos);
        }
    }
}
