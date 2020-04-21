using System;
using System.Collections.Generic;
using BusinessMobil.App.Model;
using BusinessMobil.App.ViewModel;
using Xamarin.Forms;

namespace BusinessMobil.App.Views
{
    public partial class CarnetViewPage : ContentPage
    {
        public CarnetViewPage(DatosScanerModel carnet)
        {
            InitializeComponent();
            BindingContext = new CarnetViewViewModel(carnet);
        }
    }
}
