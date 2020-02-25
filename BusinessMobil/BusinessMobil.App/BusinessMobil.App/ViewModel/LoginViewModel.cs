﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BusinessMobil.App.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(INavigation navigation)
        {
            Navigation = navigation;
            EntrarCommand = new Command( async () => await Entrar());
        }
        public string EMail { get; set; }
        public string Password { get; set; }

        public ICommand EntrarCommand { get; set; }

        async Task Entrar()
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}
