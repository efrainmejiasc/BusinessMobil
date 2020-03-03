using System;
using BusinessMobil.App.Helpers;
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
            //if(string.IsNullOrEmpty(Settings.Email))
                MainPage = new NavigationPage(new Login());
            //else
              //  MainPage = new NavigationPage(new MainPage());
           
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
