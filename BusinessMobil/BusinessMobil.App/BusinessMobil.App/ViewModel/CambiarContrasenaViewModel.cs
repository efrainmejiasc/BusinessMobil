using BusinessMobil.App.Controls.Interface;
using BusinessMobil.App.Helpers;
using BusinessMobil.App.Model;
using BusinessMobil.App.Service;
using BusinessMobil.App.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BusinessMobil.App.ViewModel
{
    public class CambiarContrasenaViewModel : BaseViewModel
    {
        Api api = new Api();

        public CambiarContrasenaViewModel()
        {
            Email = "tuidentidad@hotmail.com";
            Password = "Tu.123456";
            PasswordConfirmacion = Password;
        }

        public ICommand ActualizarCommand => new Command(async () => await Actualizar());
        public ICommand CancelarCommand => new Command(async () => await Navigation.PushAsync(new Login()));

        string email;
        public string Email
        {
            get => email;
            set => SetValue(ref email, value);
        }

        string password;
        public string Password
        {
            get => password;
            set => SetValue(ref password, value);
        }

        string passwordConfirmacion;
        public string PasswordConfirmacion
        {
            get => passwordConfirmacion;
            set => SetValue(ref passwordConfirmacion, value);
        }

        #region Funciones
        private async Task Actualizar()
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

            if (string.IsNullOrEmpty(PasswordConfirmacion))
            {
                await Application.Current.MainPage.DisplayAlert("Validaciôn", "Ingrese la Contraseña de Confirmación.", "Ok");
                return;
            }

            if (Password != PasswordConfirmacion)
            {
                await Application.Current.MainPage.DisplayAlert("Validaciôn", "Las Contraseñas deben ser identicas.", "Ok");
                return;
            }

            DependencyService.Get<ILodingPageService>().ShowLoadingPage();
            await Task.Delay(100);

            var user = new LoginModel { Email = Email, Password = Password, User = Email };
            var result = await api.PutRespondeAsync($"UserApi/UpdateUser", user);

            if (!result.IsSuccess)
            {
                DependencyService.Get<ILodingPageService>().HideLoadingPage();
                await Application.Current.MainPage.DisplayAlert("Error.!", "Error al actualizar la Contraseña.!", "OK");
                return;
            }

            DependencyService.Get<ILodingPageService>().HideLoadingPage();
            await Application.Current.MainPage.DisplayAlert("Actualizar Contraseña", "Contraseña actualizada.!", "Ok");
            await Application.Current.MainPage.Navigation.PushAsync(new Login());
        }
        #endregion
    }
}
