using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML
{
    public partial class Opcion
    {
        public int Id { get; set; }

        public int? Menu { get; set; }

        public int? Agrupador { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Texto { get; set; }

        public string? Pathicono { get; set; }

        public string? Url { get; set; }

        public int? Ordenmostrar { get; set; }

        public bool Activo { get; set; }
        public int? aplicacion { get; set; }

        public virtual ICollection<Accion> Accions { get; set; } = new List<Accion>();

        public virtual Agrupador? AgrupadorNavigation { get; set; }

        public virtual Menu? MenuNavigation { get; set; }

        public virtual ICollection<Rolopcion> Rolopcions { get; set; } = new List<Rolopcion>();

        public virtual ICollection<Usuarioopcion> Usuarioopcions { get; set; } = new List<Usuarioopcion>();
    }

}
