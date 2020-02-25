using System;
using BusinessMobil.App.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BusinessMobil.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new MainPage());
            MainPage = new NavigationPage(new Login());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
