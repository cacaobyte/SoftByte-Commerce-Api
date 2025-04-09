using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML.cc.Rrhh
{
    public class PositionsRrhh
    {

        public Guid IdDepartamento { get; set; }
        public string? NombreDepartamento { get; set; }

        public string NombrePuesto { get; set; } = null!;

        public string? Descripcion { get; set; }

        public string? CodigoPuesto { get; set; }

        public string? NivelJerarquico { get; set; }

        public string? TipoPuesto { get; set; }

        public decimal? SueldoBase { get; set; }

        public string? ModalidadTrabajo { get; set; }

        public bool? RequiereSupervision { get; set; }

        public bool? EsPuestoBase { get; set; }

        public int? CantidadVacantes { get; set; }

        public string? HorarioTrabajo { get; set; }

        public string? RequisitosMinimos { get; set; }

        public bool? CapacitacionRequerida { get; set; }

        public string? RiesgoLaboral { get; set; }

        public string? HerramientasUtilizadas { get; set; }

        public bool? UniformeRequerido { get; set; }

        public string? Observaciones { get; set; }

        public string? Estado { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// relación de los datos con su aplicación
        /// </summary>
        public int? Aplicación { get; set; }

        //public virtual Aplicacion? AplicaciónNavigation { get; set; }

        //public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

        //public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;
    }
}
