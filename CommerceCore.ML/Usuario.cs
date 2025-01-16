using System;
using System.Collections.Generic;

namespace CommerceCore.ML;

public partial class Usuario
{
    public string Usuario1 { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Tipo { get; set; }

    public bool? Activo { get; set; }

    public bool? ReqCambioClave { get; set; }

    public short? FrecuenciaClave { get; set; }

    public DateTime? FechaUltClave { get; set; }

    public short? MaxIntentosConex { get; set; }

    public string Clave { get; set; } = null!;

    public string? CorreoElectronico { get; set; }

    public string? Celular { get; set; }

    public string? Telefono1 { get; set; }

    public string? Telefono2 { get; set; }

    public string? Direccion { get; set; }

    public string? DocumentoIdentificacion { get; set; }

    public string? FotoUrl { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? TipoAcceso { get; set; }

    public string? TipoPersonalizado { get; set; }

    public string? Createdby { get; set; }

    public string? Updatedby { get; set; }

    public DateTime? Createdate { get; set; }

    public DateTime? Updatedate { get; set; }

    public bool? Noteexistsflag { get; set; }

    public DateTime? Recorddate { get; set; }

    public Guid? Rowpointer { get; set; }
}
