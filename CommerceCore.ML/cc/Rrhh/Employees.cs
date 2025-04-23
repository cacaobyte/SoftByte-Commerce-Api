using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML.cc.Rrhh
{
    public class EmployeesRrhh
    {

        public string Nombres { get; set; } = null!;

        public string Apellidos { get; set; } = null!;
        public int? aplicacion { get; set; }

        public string? Genero { get; set; }

        public DateOnly? FechaNacimiento { get; set; }

        public string? TipoDocumento { get; set; }

        public string? NumeroDocumento { get; set; }

        public string? Nacionalidad { get; set; }

        public string? Correo { get; set; }

        public string? Telefono1 { get; set; }

        public string? Telefono2 { get; set; }

        public string? Direccion { get; set; }

        public string? DepartamentoResidencia { get; set; }

        public string? MunicipioResidencia { get; set; }

        public string? Foto { get; set; }

        public DateOnly? FechaIngreso { get; set; }

        public DateOnly? FechaEgreso { get; set; }

        public string? TipoContrato { get; set; }

        public string? TipoEmpleado { get; set; }

        public string? Jornada { get; set; }

        public decimal? Salario { get; set; }

        public string? EstadoEmpleado { get; set; }

        public string? TipoSangre { get; set; }

        public string? ContactoEmergenciaNombre { get; set; }

        public string? ContactoEmergenciaParentesco { get; set; }

        public string? ContactoEmergenciaTelefono { get; set; }

        public string? ContactoEmergenciaDireccion { get; set; }

        public string? Observaciones { get; set; }

        public string? NotasInternas { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? Puesto { get; set; }

        public Guid? Departamento { get; set; }

        //public virtual Departamento? DepartamentoNavigation { get; set; }

        //public virtual Puesto? PuestoNavigation { get; set; }
    }
}
