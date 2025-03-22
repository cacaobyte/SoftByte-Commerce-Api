using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommerceCore.ML.cc.sale.Quotes
{
    public class CreateQuoteRequest
    {
        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;

        public string? ClienteId { get; set; } = "";
        public int? aplicacion { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreCliente { get; set; } = "N/A";

        [Required]
        [MaxLength(100)]
        public string ApellidoCliente { get; set; } = "N/A";

        [MaxLength(100)]
        public string? Correo { get; set; } = "";

        [MaxLength(20)]
        public string? Telefono { get; set; } = "";

        [Required]
        [MaxLength(20)]
        public string TipoPago { get; set; } = "Efectivo";

        [Range(0, 999.99)]
        public decimal DescuentoCliente { get; set; } = 0;

        [Range(0, 9999999.99)]
        public decimal Subtotal { get; set; } = 0;

        [Range(0, 9999.99)]
        public decimal DescuentoTotal { get; set; } = 0;

        [Range(0, 9999999.99)]
        public decimal Impuestos { get; set; } = 0;

        [Range(0, 9999999.99)]
        public decimal Total { get; set; } = 0;

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = "Pendiente";

        [Required]
        [MaxLength(10)]
        public string Moneda { get; set; } = "USD";

        [Required]
        [MaxLength(20)]
        public string Origen { get; set; } = "Desconocido";

        [Required]
        [MaxLength(255)]
        public string UsuarioCreador { get; set; } = "Sistema";

        [MaxLength(255)]
        public string? UsuarioActualiza { get; set; }

        [MaxLength(255)]
        public string? UsuarioAprueba { get; set; }

        public string? Notas { get; set; } = "";

        [Required]
        public List<CreateQuoteDetailRequest> Detalles { get; set; } = new List<CreateQuoteDetailRequest>();
    }

    public class CreateQuoteDetailRequest
    {
        [Required]
        public string? IdArticulo { get; set; }
        public int? aplicacion { get; set; }

        [Required]
        [MaxLength(150)]
        public string NombreArticulo { get; set; } = "";

        [Range(0.01, 9999999.99)]
        public decimal PrecioUnitario { get; set; } = 0.01M;

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
        public int Cantidad { get; set; } = 1;

        [Range(0, 999.99)]
        public decimal DescuentoAplicado { get; set; } = 0;

        [Range(0, 9999999.99)]
        public decimal Subtotal { get; set; } = 0;

        [Range(0, 9999999.99)]
        public decimal Impuestos { get; set; } = 0;

        [Range(0, 9999999.99)]
        public decimal Total { get; set; } = 0;

        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(255)]
        public string UsuarioCreador { get; set; } = "Sistema";

        [MaxLength(255)]
        public string? UsuarioActualiza { get; set; }
    }
}
