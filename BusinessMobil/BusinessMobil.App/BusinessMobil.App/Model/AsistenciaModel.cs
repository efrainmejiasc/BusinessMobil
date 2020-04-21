using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessMobil.App.Model
{
    public class AsistenciaModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string Grado { get; set; }
        public string Grupo { get; set; }
        public int IdCompany { get; set; }
        public string Materia { get; set; }
        public string Foto { get; set; }
        public string DniAdm { get; set; }
    }
}
