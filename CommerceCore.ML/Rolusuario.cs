using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML
{
    public partial class Rolusuario
    {
        public int Rol { get; set; }

        public int Usuario { get; set; }

        public bool Superusuario { get; set; }

        public virtual Rol RolNavigation { get; set; } = null!;
    }
}
