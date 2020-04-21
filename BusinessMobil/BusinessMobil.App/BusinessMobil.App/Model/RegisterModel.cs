using System;
namespace BusinessMobil.App.Model
{
    public class RegisterModel
    {
        public RegisterModel()
        {

        }
        public string Email { get; set; }
        public string User { get; set; }
        public string Dni { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
    }
}