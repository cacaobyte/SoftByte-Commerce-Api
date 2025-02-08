using System;
using System.Collections.Generic;

namespace CommerceCore.ML;

public partial class Regione
{
    public int IdRegion { get; set; }

    public string Nombre { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? TipoRegion { get; set; }

    public bool Estatus { get; set; }

    public DateTime? FechaCreacion { get; set; }
    public decimal latitud { get; set; }
    public decimal longitud { get; set; }
}
