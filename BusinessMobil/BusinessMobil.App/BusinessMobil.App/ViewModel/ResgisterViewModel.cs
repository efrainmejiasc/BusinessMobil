using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessMobil.App.Model;
using BusinessMobil.App.Service;
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

            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Campo Obligatorio", "", "Ok");
                return;
            }

            var register = new RegisterModel { Email = Email, User = User, Password = Password };
            var result = await api.PostRespondeAsync<RegisterModel>("/UserApi/CreateUser",register);
            if(!result.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error!", result.Message,"Ok");
                return;
            }
            var token = JsonConvert.DeserializeObject<Token>(result.Result.ToString());

            await Navigation.PushAsync(new MainPage());
        }
    }
}
