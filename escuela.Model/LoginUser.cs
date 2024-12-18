using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPIMySQL.Model
{
    public class UserLoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginResponse
    {
        public int nCodigo { get; set; }
        public string sMensaje { get; set; }
        public UserInformation data { get; set; }
    }

    public class UserInformation
    {
        public int UserId { get; set; }
        public string user { get; set; }
    }
}
