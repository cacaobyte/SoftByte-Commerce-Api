using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML
{
    public partial class Agrupador
    {
        public int Id { get; set; }

        public int? Menu { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Texto { get; set; }

        public string? Pathicono { get; set; }

        public int? Ordenmostrar { get; set; }

        public bool Activo { get; set; }

        public virtual Menu? MenuNavigation { get; set; }

        public virtual ICollection<Opcion> Opcions { get; set; } = new List<Opcion>();
    }

}
