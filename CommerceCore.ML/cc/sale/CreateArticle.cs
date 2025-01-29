using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML.cc.sale
{
    public partial class CreateArticle
    {


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
        public string? Clasificacion { get; set; }

        // public virtual ICollection<ExistenciaBodega> ExistenciaBodegas { get; set; } = new List<ExistenciaBodega>();
    }
}
