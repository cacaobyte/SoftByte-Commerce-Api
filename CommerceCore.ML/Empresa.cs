using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML
{
    public partial class Empresa
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public bool Activo { get; set; }

        public virtual ICollection<Aplicacion> Aplicacions { get; set; } = new List<Aplicacion>();

    //    public virtual ICollection<Externaltoken> Externaltokens { get; set; } = new List<Externaltoken>();
    }
}
