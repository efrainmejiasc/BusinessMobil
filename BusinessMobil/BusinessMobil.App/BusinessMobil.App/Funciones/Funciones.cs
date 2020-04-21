using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessMobil.App.Funciones
{
    public class Funciones
    {
        //public Xamarin.Forms.ImageSource Base64ToImage(string base64String) => Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(base64String)));
        public Xamarin.Forms.ImageSource Base64ToImage(string base64String)
        {
            var baseS = Convert.FromBase64String(base64String);
            return Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(baseS));
        }

        public string base64Decode(string base64Encode)
        {
           
            byte[] data = Convert.FromBase64String(base64Encode);
            return ASCIIEncoding.ASCII.GetString(data);
        }
    }
}
