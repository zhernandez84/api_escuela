using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPIMySQL.Model
{
    public class Materia
    {
        public int nIdMateria { get; set; }
        public string sMateria { get; set; }

    }

    public class SearchMateriaResponse
    {
        public int nCodigo { get; set; }
        public string sMensaje { get; set; }
        public List<Materia> materia { get; set; }
    }
}
