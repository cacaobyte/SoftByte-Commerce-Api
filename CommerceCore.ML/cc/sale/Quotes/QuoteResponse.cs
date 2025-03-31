using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML.cc.sale.Quotes
{
    public class QuoteResponse
    {
        public int IdCotizacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActualizacion { get; set; }

        public string? ClienteId { get; set; }

        public string NombreCliente { get; set; } = string.Empty;

        public string ApellidoCliente { get; set; } = string.Empty;

        public string? Correo { get; set; }

        public string? Telefono { get; set; }

        public string TipoPago { get; set; } = "";

        public decimal DescuentoCliente { get; set; }

        public decimal Subtotal { get; set; }

        public decimal DescuentoTotal { get; set; }

        public decimal Impuestos { get; set; }

        public decimal Total { get; set; }

        public string Estado { get; set; } = "Pendiente";

        public string Moneda { get; set; } = "Q";

        public string Origen { get; set; } = "Desconocido";

        public string UsuarioCreador { get; set; } = "Sistema";

        public string? UsuarioActualiza { get; set; }

        public string? UsuarioAprueba { get; set; }

        public string? Notas { get; set; }

        public int? aplicacion { get; set; }

        public List<QuoteDetailResponse> Detalles { get; set; } = new();
    }

    public class QuoteDetailResponse
    {
        public int IdDetalleCotizacion { get; set; }

        public int IdCotizacion { get; set; }

        public string IdArticulo { get; set; } = string.Empty;

        public string NombreArticulo { get; set; } = string.Empty;

        public decimal PrecioUnitario { get; set; }

        public int Cantidad { get; set; }

        public decimal DescuentoAplicado { get; set; }

        public decimal Subtotal { get; set; }

        public decimal Impuestos { get; set; }

        public decimal Total { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActualizacion { get; set; }

        public string UsuarioCreador { get; set; } = "Sistema";

        public string? UsuarioActualiza { get; set; }

        public int? aplicacion { get; set; }
    }
}
