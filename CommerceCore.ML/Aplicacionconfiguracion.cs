using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML
{
    public partial class Aplicacionconfiguracion
    {
        public int Aplicacion { get; set; }

        public string? Logo { get; set; }

        public string? Nombremostrar { get; set; }

        public string? Cssclass { get; set; }

        public string? Textboton { get; set; }

        public bool? Mostrarnombreempresa { get; set; }

        public virtual Aplicacion AplicacionNavigation { get; set; } = null!;
    }

}
