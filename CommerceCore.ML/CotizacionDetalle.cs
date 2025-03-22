using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML
{
    public partial class CotizacionDetalle
    {
        public int IdDetalleCotizacion { get; set; }

        public int IdCotizacion { get; set; }

        public string IdArticulo { get; set; }

        public string NombreArticulo { get; set; } = null!;

        public decimal PrecioUnitario { get; set; }

        public int Cantidad { get; set; }

        public decimal DescuentoAplicado { get; set; }

        public decimal Subtotal { get; set; }

        public decimal Impuestos { get; set; }

        public decimal Total { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActualizacion { get; set; }

        public string UsuarioCreador { get; set; } = null!;

        public string? UsuarioActualiza { get; set; }
        public int? aplicacion { get; set; }
    }

}
