using System;
namespace BusinessMobil.App.Model
{
    public class RegisterDeviceModel
    {
        public RegisterDeviceModel()
        {
        }
        public string User { get; set; }
        public int IdCompany { get; set; }
        public string NameCompany { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Codigo { get; set; }
        public int NumberDevice { get; set; }
        public int IdUserApi { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }

    }
}
