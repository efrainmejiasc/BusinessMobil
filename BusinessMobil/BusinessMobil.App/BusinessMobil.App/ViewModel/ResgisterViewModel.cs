using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessMobil.App.Controls.Interface;
using BusinessMobil.App.Model;
using BusinessMobil.App.Service;
using BusinessMobil.App.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BusinessMobil.App.ViewModel
{
    public class ResgisterViewModel : BaseViewModel
    {
        Api api;
        public ResgisterViewModel()
        {
            api = new Api();
            RegistarCommand = new Command(async () => await Registar());
        }

        string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        string user;
        public string User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }

        string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public ICommand RegistarCommand { get; set; }

        async Task Registar()
        {
            try
            {
                if (string.IsNullOrEmpty(Email))
                {
                    await Application.Current.MainPage.DisplayAlert("Campo Obligatorio", "Introdusca el Email!", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(User))
                {
                    await Application.Current.MainPage.DisplayAlert("Campo Obligatorio", "Instrodusca un Usuario!", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(Password))
                {
                    await Application.Current.MainPage.DisplayAlert("Campo Obligatorio", "Instrodusca la Contraseña!", "Ok");
                    return;
                }
                DependencyService.Get<ILodingPageService>().ShowLoadingPage();
                var register = new RegisterModel { Email = Email, User = User, Password = Password };
                var result = await api.PostRespondeAsync<RegisterModel>("UserApi/CreateUser", register);
                if (!result.IsSuccess)
                {
                    DependencyService.Get<ILodingPageService>().HideLoadingPage();
                    await Application.Current.MainPage.DisplayAlert("Error!", result.Message, "Ok");
                    return;
                }
                DependencyService.Get<ILodingPageService>().HideLoadingPage();
                await Application.Current.MainPage.DisplayAlert("Registro de Usuario", "Se ha registrado con exito!", "Ok");
                await Navigation.PushAsync(new Login());
            }
            catch (Exception ex)
            {
                DependencyService.Get<ILodingPageService>().HideLoadingPage();
            }
        }
    }
}
