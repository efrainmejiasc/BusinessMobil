using System;
using BusinessMobil.App.Helpers;
using BusinessMobil.App.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BusinessMobil.App
{
    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }

        public App()
        {
            InitializeComponent();
            if(!string.IsNullOrEmpty(Settings.Email))
                Application.Current.MainPage = new MasterPage();
            else
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
