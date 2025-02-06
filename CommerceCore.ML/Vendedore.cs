using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML
{
    public partial class Vendedore
    {
        public int Vendedor { get; set; }

        public string? Nombres { get; set; }

        public string? Apellidos { get; set; }

        public string? Bodega { get; set; }

        public string? Nacionalidad { get; set; }

        public string? Dpi { get; set; }

        public int? Edad { get; set; }

        public string? Telefono1 { get; set; }

        public string? Telefono2 { get; set; }

        public string? Correo { get; set; }

        public string? Direccion { get; set; }

        public string? Genero { get; set; }

        public DateOnly? FechaNacimiento { get; set; }

        public DateOnly? FechaIngreso { get; set; }

        public string? Profesion { get; set; }

        public string? Empresa { get; set; }

        public decimal? SueldoBase { get; set; }

        public decimal? Comision { get; set; }

        public decimal? MetaVentas { get; set; }

        public decimal? Bono { get; set; }

        public string? HorarioTrabajo { get; set; }

        public string? TipoContrato { get; set; }

        public DateTime? UltimaVenta { get; set; }

        public string? Observaciones { get; set; }

        public string? Comentarios { get; set; }

        public DateTime? Recorddate { get; set; }

        public DateTime? Updatedate { get; set; }

        public string? Createby { get; set; }

        public string? Updateby { get; set; }

        public string? Estado { get; set; }

        public bool? Activo { get; set; }

        public bool? Notificar { get; set; }

        public virtual Bodega? BodegaNavigation { get; set; }
    }


}
