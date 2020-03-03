using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessMobil.App.Helpers;
using BusinessMobil.App.Service;
using Newtonsoft.Json;
using Plugin.DeviceInfo;
using Xamarin.Essentials;
using Xamarin.Forms;
using BusinessMobil.App.Model;

namespace BusinessMobil.App.ViewModel
{
    public class RegisterDeviceViewModel : BaseViewModel
    {
        Api api;

        public RegisterDeviceViewModel()
        {
            Phone = CrossDevice.Device.DeviceId;
            Codigo = Settings.IdCompany.ToString();
            User = Settings.User;
            Email = Settings.Email;
            api = new Api();
            RegisterCommand = new Command(async () => await RegisterDevice());
        }

        public ICommand RegisterCommand { get; set; }

        string phone;
        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                OnPropertyChanged();
            }
        }

        string user;
        public string User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged();
            }
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

        string codigo;
        public string Codigo
        {
            get => codigo;
            set
            {
                codigo = value;
                OnPropertyChanged();
            }
        }

        double dni;
        public double Dni
        {
            get => dni;
            set
            {
                dni = value;
                OnPropertyChanged();
            }
        }

        async Task  RegisterDevice()
        {
            var register = new RegisterDeviceModel()
            {
                Codigo = Codigo,
                User = User,
                Email = Email,
                Phone = Phone,
                Dni = Dni
            };
            var result = await api.PostRespondeAsync("DeviceApi/RegisterDevice", register,new Token() { access_token =  Settings.Token, type_token = Settings.TypeToken});
            if (!result.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error!", result.Message, "Ok");
                return;
            }
            
            await Navigation.PushAsync(new MainPage());
        }
    }
}
