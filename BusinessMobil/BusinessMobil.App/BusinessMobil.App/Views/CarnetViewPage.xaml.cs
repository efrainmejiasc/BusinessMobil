using System;
using System.Collections.Generic;
using BusinessMobil.App.ViewModel;
using Xamarin.Forms;

namespace BusinessMobil.App.Views
{
    public partial class CarnetViewPage : ContentPage
    {
        public CarnetViewPage(string carnet)
        {
            InitializeComponent();
            BindingContext = new CarnetViewViewModel(carnet);
        }
    }
}
