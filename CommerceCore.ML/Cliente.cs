using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML
{
    public partial class Cliente
    {
        public string Cliente1 { get; set; } = null!;

        public string PrimerNombre { get; set; } = null!;

        public string? SegundoNombre { get; set; }

        public string? TercerNombre { get; set; }

        public string? PrimerApellido { get; set; }

        public string? SegundoApellido { get; set; }
        public string? foto { get; set;  }

        public bool? Cf { get; set; }

        public string? Dpi { get; set; }

        public string? Genero { get; set; }

        public DateOnly? FechaNacimiento { get; set; }

        public string? Celular { get; set; }

        public string? Email { get; set; }

        public string? Celular2 { get; set; }

        public string? Nit { get; set; }

        public string? NombreFactura { get; set; }

        public string? Direccion { get; set; }

        public string? Colonia { get; set; }

        public string? Zona { get; set; }

        public string? Departamento { get; set; }

        public string? Municipio { get; set; }

        public string? EstadoCivil { get; set; }

        public string? Nacionalidad { get; set; }

        public string? Profesion { get; set; }

        public string? Empresa { get; set; }

        public DateTime? Recorddate { get; set; }

        public string? Createby { get; set; }

        public string? Updateby { get; set; }

        public DateTime? Createdate { get; set; }

        public DateTime? Updatedate { get; set; }

        public string? Moneda { get; set; }

        public decimal? Descuento { get; set; }

        public bool? Activo { get; set; }

        public int? Edad { get; set; }

        public bool? Notificar { get; set; }
        public int? aplicacion { get; set;  }
    }

}
