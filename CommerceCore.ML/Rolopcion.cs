using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML
{
    public partial class Rolopcion
    {
        public int Id { get; set; }

        public int? Rol { get; set; }

        public int? Opcion { get; set; }

        public bool Permitido { get; set; }

        public virtual Opcion? OpcionNavigation { get; set; }

        public virtual Rol? RolNavigation { get; set; }

        public virtual ICollection<Rolopcionaccion> Rolopcionaccions { get; set; } = new List<Rolopcionaccion>();
    }
}
