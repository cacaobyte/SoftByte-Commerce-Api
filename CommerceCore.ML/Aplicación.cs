using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML
{
    public partial class Aplicacion
    {
        public int Id { get; set; }

        public string Appkey { get; set; } = null!;

        public int? Empresa { get; set; }

        public string Nombre { get; set; } = null!;

        public bool Activo { get; set; }

        public string? Accesstoken { get; set; }

        public DateTime? Cookieexpire { get; set; }

        public bool? Interno { get; set; }
        public string? plan {  get; set; }

        public virtual Aplicacionconfiguracion? Aplicacionconfiguracion { get; set; }

        public virtual ICollection<Aplicacionhost> Aplicacionhosts { get; set; } = new List<Aplicacionhost>();

        public virtual Empresa? EmpresaNavigation { get; set; }

        public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

        public virtual ICollection<Rol> Rols { get; set; } = new List<Rol>();
    }

}
