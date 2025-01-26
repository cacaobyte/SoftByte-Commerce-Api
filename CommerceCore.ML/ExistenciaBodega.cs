using System;
using System.Collections.Generic;

namespace CommerceCore.ML;

public partial class ExistenciaBodega
{
    public string Articulo { get; set; } = null!;

    public string Bodega { get; set; } = null!;

    public decimal? ExistenciaMinima { get; set; }

    public decimal? ExistenciaMaxima { get; set; }

    public decimal? PuntoDeReorden { get; set; }

    public decimal? CantDisponible { get; set; }

    public decimal? CantReservada { get; set; }

    public decimal? CantNoAprobada { get; set; }

    public decimal? CantVencida { get; set; }

    public decimal? CantTransito { get; set; }

    public decimal? CantProduccion { get; set; }

    public decimal? CantPedida { get; set; }

    public decimal? CantRemitida { get; set; }

    public bool? Congelado { get; set; }

    public DateTime? FechaCong { get; set; }

    public bool? BloqueaTrans { get; set; }

    public DateTime? FechaDescong { get; set; }

    public string? Createdby { get; set; }

    public string? Updatedby { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechaactualizacion { get; set; }

    public decimal? CostoUntPromedioLoc { get; set; }

    public decimal? CostoUntPromedioDol { get; set; }

    public decimal? CostoUntEstandarLoc { get; set; }

    public decimal? CostoUntEstandarDol { get; set; }

    public decimal? CostoPromComparativoLoc { get; set; }

    public decimal? CostoPromComparativoDolar { get; set; }

    public virtual Articulo ArticuloNavigation { get; set; } = null!;

    public virtual Bodega BodegaNavigation { get; set; } = null!;
}