using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML
{
    public partial class Usuarioopcionaccion
    {
        public int Usuarioopcion { get; set; }

        public int Accion { get; set; }

        public bool Permitido { get; set; }

        public virtual Accion AccionNavigation { get; set; } = null!;

        public virtual Usuarioopcion UsuarioopcionNavigation { get; set; } = null!;
    }
}
