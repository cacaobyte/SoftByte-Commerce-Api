using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML.Commerce
{
    public class ArticuloAgrupadoDto
    {
        public string Articulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string? Foto { get; set; }
        public string? Categoria { get; set; }
        public string? Clasificacion { get; set; }

        // Aquí agrupamos por código de bodega como clave
        public Dictionary<string, VarianteBodegaDto> VariantesPorBodega { get; set; } = new();
    }

    public class VarianteBodegaDto
    {
        public string Bodega { get; set; } = null!;
        public decimal? Precio { get; set; } // Lo puedes ajustar según de dónde venga
        public decimal? Disponible { get; set; }
        public string? Ubicacion { get; set; } // Puede ser dirección, región, etc.
    }

}
