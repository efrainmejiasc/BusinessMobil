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
            //Navigation = Application.Current.MainPage.Navigation;
        }

        public string Email { get; set; }
        public string Password{ get; set; }

        public ICommand EntrarCommand { get; set; }
        public ICommand RegistrarseCommand { get; set; }

        async Task Entrar()
        {
            var user = new LoginModel { Email = Email, Password = Password, User = Email };
            var result = await api.PostRespondeAsync<LoginModel>("/UserApi/Login", user);

            if (!result.IsSuccess)
            {
                IsRunning = false;
                await Application.Current.MainPage.DisplayAlert("Error.!", "El usuario o contraseña no existe.!", "OK");
                return;
            }

            await Navigation.PushAsync(new MainPage());
        }
    }
}
