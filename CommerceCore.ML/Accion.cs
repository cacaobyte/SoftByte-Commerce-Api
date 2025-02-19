using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML
{
    public partial class Accion
    {
        public int Id { get; set; }

        public int? Opcion { get; set; }

        public string Nombre { get; set; } = null!;

        public bool Activo { get; set; }
        public int? aplicacion { get; set; }

        public virtual Opcion? OpcionNavigation { get; set; }

        public virtual ICollection<Rolopcionaccion> Rolopcionaccions { get; set; } = new List<Rolopcionaccion>();

        public virtual ICollection<Usuarioopcionaccion> Usuarioopcionaccions { get; set; } = new List<Usuarioopcionaccion>();
    }

}
