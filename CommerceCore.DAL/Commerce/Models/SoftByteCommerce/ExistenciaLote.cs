using System;
using System.Collections.Generic;

namespace CommerceCore.DAL.Commerce.Models.SoftByteCommerce;

public partial class ExistenciaLote
{
    public string Bodega { get; set; } = null!;

    public string Articulo { get; set; } = null!;

    public string? Localizacion { get; set; }

    public string Lote { get; set; } = null!;

    public decimal? CantDisponible { get; set; }

    public decimal? CantReservada { get; set; }

    public decimal? CantNoAprobada { get; set; }

    public decimal? CantVencida { get; set; }

    public decimal? CantRemitida { get; set; }

    public decimal? CostoUntPromedioLoc { get; set; }

    public decimal? CostoUntPromedioDol { get; set; }

    public decimal? CostoUntEstandarLoc { get; set; }

    public decimal? CostoUntEstandarDol { get; set; }

    public string? Createdby { get; set; }

    public string? Updatedby { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechaactualizacion { get; set; }
}
