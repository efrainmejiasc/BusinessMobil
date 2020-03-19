using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessMobil.App.ViewModel
{
    public class AsistenciaClase
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public int IdCompany { get; set; }
        public bool Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string DniAdm { get; set; }
        public string Materia { get; set; }
        public int Turno { get; set; }
        public string EmailSend { get; set; }
    }
}
