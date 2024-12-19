using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPIMySQL.Model
{
    public class Alumno
    {
        public int nIdAlumno { get; set; }
        public int nEstatus { get; set; }
        public string sIdAlumno { get; set; }
        public string sNombre { get; set; }
        public string sPaterno { get; set; }
        public string sMaterno { get; set; }
        public string dFecNacimiento { get; set; }
        public string sGenero { get; set; }

    }

    public class AlumnoRespose
    {
        public int nIdAlumno { get; set; }
        public int nEstatus { get; set; }
        public string sEstatus { get; set; }
        public string sIdAlumno { get; set; }
        public string sNombre { get; set; }
        public string sPaterno { get; set; }
        public string sMaterno { get; set; }
        public DateTime dFecNacimiento { get; set; }
        public string sGenero { get; set; }

    }

    public class RegisterAlumnoResponse
    {
        public int nCodigo { get; set; }
        public string sMensaje { get; set; }
    }

    public class SearchAlumnoResponse
    {
        public int nCodigo { get; set; }
        public string sMensaje { get; set; }
        public List<AlumnoRespose> alumnoRespose { get; set; }
    }
}
