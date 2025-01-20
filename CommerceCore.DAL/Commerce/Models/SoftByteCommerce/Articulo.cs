using System;
using System.Collections.Generic;

namespace CommerceCore.DAL.Commerce.Models.SoftByteCommerce;

public partial class Articulo
{
    public string Articulo1 { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string? Categoria { get; set; }

    public decimal Precio { get; set; }

    public decimal? PesoNeto { get; set; }

    public decimal? PesoBruto { get; set; }

    public decimal? Volumen { get; set; }

    public bool? Activo { get; set; }

    public string? Createdby { get; set; }

    public string? Updatedby { get; set; }

    public string? SubCategoria { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechaactualizacion { get; set; }

    public virtual ICollection<ExistenciaBodega> ExistenciaBodegas { get; set; } = new List<ExistenciaBodega>();
}
