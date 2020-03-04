using System;
using BusinessMobil.App.Helpers;
using BusinessMobil.App.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BusinessMobil.App
{
    public partial class App : Application
    {
        public static INavigation Navigator { get; internal set; }

        public App()
        {
            InitializeComponent();
            //NavigationPage navPage = new NavigationPage
            //{
            //    BarBackgroundColor = Color.FromHex("#1FBED6"),
            //    BarTextColor = Color.FromHex("#000000")
            //};
            //if(string.IsNullOrEmpty(Settings.Email))
            MainPage = new NavigationPage(new Login());
                //{
                //    BarBackgroundColor = Color.FromHex("#FFFFFF"),
                //    BarTextColor = Color.FromHex("#F4F4F4")
                //};
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
