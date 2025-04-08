using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML.cc.Rrhh
{
    public class DepartamentsRrhh
    {

        public string NombreDepartamento { get; set; } = null!;

        public string? Descripcion { get; set; }

        public string? CodigoDepartamento { get; set; }

        public string? UbicacionFisica { get; set; }

        public string? CorreoContacto { get; set; }

        public string? ExtensionTelefonica { get; set; }

        public string? Estado { get; set; }

        public string? Observaciones { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// relación con la aplicación
        /// </summary>
        public int? Aplicación { get; set; }

   
    }
}
