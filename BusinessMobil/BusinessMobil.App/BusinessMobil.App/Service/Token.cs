using System;
namespace BusinessMobil.App.Service
{
    public class Token
    {
        public Token()
        {
        }
        
        public string access_token { get; set; }
        public string expire_token { get; set; }
        public string type_token{ get; set; }
        public string refresh_token { get; set; }
        public string email{ get; set; }
        public string user { get; set; }
        public int idCompany{ get; set; }
        public bool status{ get; set; }
        public int idTypeUser{ get; set; }
        public string dni { get; set; }
        public string id { get; set; }
    }
}
