using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML
{
    public partial class Rol
    {
        public int Id { get; set; }

        public int? Aplicacion { get; set; }

        public string Nombre { get; set; } = null!;

        public bool Activo { get; set; }

        public virtual Aplicacion? AplicacionNavigation { get; set; }

        public virtual ICollection<Rolopcion> Rolopcions { get; set; } = new List<Rolopcion>();

        public virtual ICollection<Rolusuario> Rolusuarios { get; set; } = new List<Rolusuario>();
    }

}
