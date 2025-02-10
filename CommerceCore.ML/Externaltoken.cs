using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML
{
    public partial class Externaltoken
    {
        public int Usuario { get; set; }

        public string Llave { get; set; } = null!;

        public bool Activo { get; set; }

        public int? Empresa { get; set; }

        public DateTime? Expire { get; set; }

        // Propiedades de navegación
        public virtual Empresa? EmpresaNavigation { get; set; }
    }

}
