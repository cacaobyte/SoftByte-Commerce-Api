using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML
{
    public partial class Cotizacione
    {
        public int IdCotizacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActualizacion { get; set; }

        public string? ClienteId { get; set; }

        public string NombreCliente { get; set; } = null!;

        public string ApellidoCliente { get; set; } = null!;

        public string? Correo { get; set; }

        public string? Telefono { get; set; }

        public string TipoPago { get; set; } = null!;

        public decimal DescuentoCliente { get; set; }

        public decimal Subtotal { get; set; }

        public decimal DescuentoTotal { get; set; }

        public decimal Impuestos { get; set; }

        public decimal Total { get; set; }

        public string Estado { get; set; } = null!;

        public string Moneda { get; set; } = null!;

        public string Origen { get; set; } = null!;

        public string UsuarioCreador { get; set; } = null!;

        public string? UsuarioActualiza { get; set; }

        public string? UsuarioAprueba { get; set; }

        public string? Notas { get; set; }
    }

}
