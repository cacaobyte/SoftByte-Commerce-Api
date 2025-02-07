using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML
{
    public partial class Faq
    {
        public int Id { get; set; }

        public string Pregunta { get; set; } = null!;

        public string Respuesta { get; set; } = null!;

        public string? Categoria { get; set; }

        public string? Subcategoria { get; set; }

        public string? Keywords { get; set; }

        public int? Visitas { get; set; }

        public int? Importancia { get; set; }

        public int? Orden { get; set; }

        public bool? Estado { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public string? CreadoPor { get; set; }

        public string? ActualizadoPor { get; set; }
    }

}
