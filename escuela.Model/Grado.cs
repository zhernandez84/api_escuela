using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPIMySQL.Model
{
    public class Grado
    {
        public int nIdGrado { get; set; }
        public string sGrado { get; set; }

    }

    public class SearchGradoResponse
    {
        public int nCodigo { get; set; }
        public string sMensaje { get; set; }
        public List<Grado> grado { get; set; }
    }
}
