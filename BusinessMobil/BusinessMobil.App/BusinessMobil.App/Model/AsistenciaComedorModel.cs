using System;
namespace BusinessMobil.App.Model
{
    public class AsistenciaComedorModel
    {
        public AsistenciaComedorModel()
        {
        }
        
        public int Id { get; set; }
        public string Dni { get; set; }
        public int IdCompany { get; set; }
        public bool Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string DniAdm { get; set; }
        public string Turno { get; set; }
    }
}
