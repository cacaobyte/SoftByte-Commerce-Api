using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML.cc.Security.Users
{
    /// <summary>
    /// Clase para el objeto de solicitud de inicio de sesión
    /// </summary>
    public class UserLoginRequest
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }

}
