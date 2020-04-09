using System;
namespace BusinessMobil.App.Model
{
    public class DatosScanerModel 
    {
        public DatosScanerModel()
        {
        }

        public int  IdCompany { get; set; }
        public GruposModel Grupo { get; set; }
        public GradosModel Grado { get; set; }
        public TurnoModel Turno { get; set; }
        public MateriaClaseModel Materia { get; set; }
        public string Dni { get; set; }
        public string DniAdm { get; set; }
        public string Observacion { get; set; }
        public string Base64Dni { get; set; }
    }
}
