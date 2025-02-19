using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML
{
    public partial class Usuarioopcion
    {
        public int Id { get; set; }

        public string Usuario { get; set; }

        public int? Opcion { get; set; }

        public bool Permitido { get; set; }

        public virtual Opcion? OpcionNavigation { get; set; }

        public virtual ICollection<Usuarioopcionaccion> Usuarioopcionaccions { get; set; } = new List<Usuarioopcionaccion>();
    }
}
