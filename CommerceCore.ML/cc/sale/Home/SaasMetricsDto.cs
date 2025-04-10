using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceCore.ML.cc.sale.Home
{
    public class SaasMetricsDto
    {
        public int TotalEmpresas { get; set; }
        public int TotalUsuarios { get; set; }
        public int TotalBodegas { get; set; }
        public int TotalClientes { get; set; }
        public int TotalCotizaciones { get; set; }
        public int TotalCategorias { get; set; }
        public int TotalVendedores { get; set; }
        public int TotalArticulos { get; set; }
        public int TotalExistenciaBodegas { get; set; }
        public int TotalRoles { get; set; }
        public DateTime FechaConsulta { get; set; }
    }

}
