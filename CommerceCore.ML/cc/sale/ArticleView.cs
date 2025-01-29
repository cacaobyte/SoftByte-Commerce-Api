using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML.cc.sale
{
    public partial class ArticleView
    {
        [Key] // Define esta propiedad como clave primaria
        public string Articulo1 { get; set; } = null!;

        public string Descripcion { get; set; } = null!;
        public string? Foto { get; set; }

        public string? Categoria { get; set; }

        public decimal Precio { get; set; }

        public decimal? PesoNeto { get; set; }

        public decimal? PesoBruto { get; set; }

        public decimal? Volumen { get; set; }

        public bool? Activo { get; set; }

        public string? Createdby { get; set; }

        public string? Updatedby { get; set; }

        public DateTime? Fechacreacion { get; set; }

        public DateTime? Fechaactualizacion { get; set; }

        /// <summary>
        /// Se usa para guardar las subcategorías de los productos
        /// </summary>
        public string? SubCategoria { get; set; }
        public string? clasificación { get; set; }

        public decimal? existencias { get; set; }
        public decimal? disponible { get; set; }
        public decimal? reservada { get; set;}
        public decimal? totalValorProducto { get; set; }
        public string? bodega { get; set; }

        // public virtual ICollection<ExistenciaBodega> ExistenciaBodegas { get; set; } = new List<ExistenciaBodega>();
    }
}
