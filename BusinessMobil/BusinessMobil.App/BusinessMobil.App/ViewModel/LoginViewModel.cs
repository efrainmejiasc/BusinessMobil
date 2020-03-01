using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessMobil.App.Model;
using BusinessMobil.App.Service;
using BusinessMobil.App.Views;
using Xamarin.Forms;

namespace BusinessMobil.App.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        Api api;
        public LoginViewModel()
        {
            api = new Api();
            EntrarCommand = new Command( async () => await Entrar());
            RegistrarseCommand = new Command(async () => await Navigation.PushAsync(new RegisterPage()));
            Email = "prueba1@gmail.com";
            Password = "Pr.123456";
        }

        string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        string password;
        public string Password
        {
            get=> password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public ICommand EntrarCommand { get; set; }
        public ICommand RegistrarseCommand { get; set; }

        async Task Entrar()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Validaciôn", "Debe ingresar el Email o Usuario.", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Validaciôn", "Ingrese la Contraseña.", "Ok");
                return;
            }

            var user = new LoginModel { Email = Email, Password = Password, User = Email };
            var result = await api.PostRespondeAsync<LoginModel>("UserApi/Login", user);

            if (!result.IsSuccess)
            {
                IsRunning = false;
                await Application.Current.MainPage.DisplayAlert("Error.!", result.Message, "OK");
                await Application.Current.MainPage.DisplayAlert("Error.!", "El usuario o contraseña no existe.!", "OK");
                return;
            }

            await Navigation.PushAsync(new MainPage());
        }
    }
}
