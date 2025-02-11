using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML
{
    public partial class Aplicacionhost
    {
        public int Id { get; set; }

        public int? Aplicacion { get; set; }

        public string Dominio { get; set; } = null!;

        public virtual Aplicacion? AplicacionNavigation { get; set; }
    }

}
