﻿using CommerceCore.ML;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



public partial class Articulo
{
    
    public string Articulo1 { get; set; } = null!;

    public string Descripcion { get; set; } = null!;
    public string? Foto { get; set; }

    public string? Categoria { get; set; }

    public decimal Precio { get; set; }

    public decimal? PesoNeto { get; set; }

    public decimal? PesoBruto { get; set; }

    public decimal? Volumen { get; set; }

    public bool? Activo { get; set; }

    public string? Createdby { get; set; }

    public string? Updatedby { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechaactualizacion { get; set; }

    /// <summary>
    /// Se usa para guardar las subcategorías de los productos
    /// </summary>
    public string? SubCategoria { get; set; }
    public string? Clasificacion { get; set; }
    public int Aplicacion { get; set; }

   // public virtual Aplicacion AplicacionNavigation { get; set; } = null!;

    // public virtual ICollection<ExistenciaBodega> ExistenciaBodegas { get; set; } = new List<ExistenciaBodega>();
}