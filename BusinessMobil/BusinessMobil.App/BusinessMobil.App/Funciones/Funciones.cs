using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessMobil.App.Funciones
{
    public class Funciones
    {
        public Xamarin.Forms.ImageSource Base64ToImage(string base64String) => Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(base64String)));
    }
}
