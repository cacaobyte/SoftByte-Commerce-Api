using System;
using System.Collections.Generic;

namespace CommerceCore.DAL.Commerce.Models;

public partial class Subcategoria
{
    public int IdSubcategoria { get; set; }

    public int IdCategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool Estatus { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
}

