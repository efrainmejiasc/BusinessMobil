using BusinessMobil.App.Helpers;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BusinessMobil.App.ViewModel
{
    public class PerfilViewModel : BaseViewModel
    {
        private MediaFile file;
        public PerfilViewModel()
        {
            Settings.FotoUser = !string.IsNullOrEmpty(Settings.FotoUser.ToString()) ? Settings.FotoUser : "noimage.png";
            ImageSource = Settings.FotoUser;
            Email = Settings.Email;
            DI = Settings.DNI;
            Telefono = Settings.Telefono;
            NombreCompleto = Settings.NombreCompleto;
        }

        #region Propiedades
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string DI { get; set; }
        #endregion

        #region ICommand
        public ICommand ChangeImageCommand => new Command(ChangeImageAsync);

        private ImageSource imageSource;
        public ImageSource ImageSource
        {
            get => imageSource;
            set => SetValue(ref imageSource, value);
        }
        #endregion

        #region Funciones
        async void ChangeImageAsync()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                var source = await Application.Current.MainPage.DisplayActionSheet(
                    "¿Donde tomas la foto?",
                    "Cancelar",
                    null,
                    "De la galería",
                    "Desde camara");

                if (source == "Cancelar")
                {
                    this.file = null;
                    return;
                }

                if (source == "Desde camara")
                {
                    this.file = await CrossMedia.Current.TakePhotoAsync(
                        new StoreCameraMediaOptions
                        {
                            Directory = "Sample",
                            Name = "photoperfil.jpg",
                            PhotoSize = PhotoSize.Small,
                        }
                    );
                }
                else
                {
                    this.file = await CrossMedia.Current.PickPhotoAsync();
                }

                if (this.file != null)
                {
                    this.ImageSource = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        return stream;
                    });
                }
                Settings.FotoUser = file.Path;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
