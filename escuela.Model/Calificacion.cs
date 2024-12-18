using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPIMySQL.Model
{
    public class Calificacion
    {
        public int nIdCalificacion { get; set; }
        public int nIdAlumno { get; set; }
        public int nIdMateria { get; set; }
        public decimal nCalificacion { get; set; }
        public int nGrado { get; set; }
        public int nMes { get; set; }
        public int nYear { get; set; }

    }

    public class RegisterCalificacionResponse
    {
        public int nCodigo { get; set; }
        public string sMensaje { get; set; }
    }

    public class SearchCalificacionRequest
    {
        public string sIdAlumno { get; set; }
        public int nGrado { get; set; }
        public int nMes { get; set; }
        public int nYear { get; set; }

    }

    public class SearchCalificacion
    {
        public int nIdCalificacion { get; set; }
        public int nIdAlumno { get; set; }
        public string sAlumno { get; set; }
        public int nIdMateria { get; set; }
        public string sMateria { get; set; }
        public decimal nCalificacion { get; set; }
        public int nGrado { get; set; }
        public string sGrado { get; set; }
        public int nMes { get; set; }
        public int nYear { get; set; }
    }

    public class SearchCalificacionResponse
    {
        public int nCodigo { get; set; }
        public string sMensaje { get; set; }
        public List<SearchCalificacion> searchCalificacion { get; set; }
    }
}
