using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML
{

    public partial class Menu
    {
        public int Id { get; set; }

        public int? Aplicacion { get; set; }

        public string Nombre { get; set; } = null!;

        public bool Activo { get; set; }

        public virtual ICollection<Agrupador> Agrupadors { get; set; } = new List<Agrupador>();

        public virtual Aplicacion? AplicacionNavigation { get; set; }

        public virtual ICollection<Opcion> Opcions { get; set; } = new List<Opcion>();
    }
}
