using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessMobil.App.Controls;
using BusinessMobil.App.Controls.Interface;
using BusinessMobil.App.Helpers;
using BusinessMobil.App.Model;
using BusinessMobil.App.Service;
using BusinessMobil.App.Views;
using Newtonsoft.Json;
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

            try
            {
                //DependencyService.Get<ILodingPageService>().InitLoadingPage(new LoadIndicator());
                DependencyService.Get<ILodingPageService>().ShowLoadingPage();
                await Task.Delay(100);

                var user = new LoginModel { Email = Email, Password = Password, User = Email };
                var result = await api.PostRespondeAsync<LoginModel>("UserApi/Login", user);

                if (!result.IsSuccess)
                {
                    IsRunning = false;
                    DependencyService.Get<ILodingPageService>().HideLoadingPage();
                    await Application.Current.MainPage.DisplayAlert("Error.!", "El usuario o contraseña no existe.!", "OK");
                    
                    return;
                }
                var token = JsonConvert.DeserializeObject<Token>(result.Result.ToString());
                Settings.Email = token.email;
                Settings.IdCompany = token.idCompany;
                Settings.IdTypeUser = token.idTypeUser;
                Settings.Password = user.Password;
                Settings.Status = token.status;
                Settings.Token = token.access_token;
                Settings.TypeToken = token.type_token;
                Settings.DNI = token.dni.Replace("{ Dni =", "").Replace(" ","").Replace("}","");
                Settings.User = token.user;
                Settings.Id = token.id;
                DependencyService.Get<ILodingPageService>().HideLoadingPage();
                Application.Current.MainPage = new MasterPage();
                
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error.!", ex.Message, "OK");
            }
        }
    }
}
