using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML
{
    public partial class Guia
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public string Contenido { get; set; } = null!;

        public string? Categoria { get; set; }

        public string? Subcategoria { get; set; }

        public string? Keywords { get; set; }

        public string? UrlExterna { get; set; }

        public string? Archivo { get; set; }

        public int? Visitas { get; set; }

        public int? Importancia { get; set; }

        public string? Idioma { get; set; }

        public string? Version { get; set; }

        public int? Orden { get; set; }

        public bool? Estado { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public string? CreadoPor { get; set; }

        public string? ActualizadoPor { get; set; }
    }


}
