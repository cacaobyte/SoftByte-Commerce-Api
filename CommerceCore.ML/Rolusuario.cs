using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML
{
    public partial class Rolusuario
    {
        public int Rol { get; set; }
        public string Usuario { get; set; }
        public bool Superusuario { get; set; }

        public virtual Rol RolNavigation { get; set; } = null!;

        // Se hace opcional para evitar errores en la inserción
        [NotMapped]
        public virtual Usuario? UsuarioNavigation { get; set; }
    }
}
