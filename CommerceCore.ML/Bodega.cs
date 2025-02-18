using System;
using System.Collections.Generic;

namespace CommerceCore.ML;

public partial class Bodega
{
    public string Bodega1 { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public string? Createdby { get; set; }

    public string? Updatedby { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechaactualizacion { get; set; }

    public string? Departamento { get; set; }

    public string? Municipio { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public bool? Bodegacentral { get; set; }

    public bool? Bodegasecundaria { get; set; }

    public string? Region { get; set; }

    public string? Correo { get; set; }
    public int Aplicacion { get; set; }  // Relación con Aplicacion
    public virtual Aplicacion AplicacionNavigation { get; set; } = null!;

    public virtual ICollection<ExistenciaBodega> ExistenciaBodegas { get; set; } = new List<ExistenciaBodega>();
}
