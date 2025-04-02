using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC;
using CommerceCore.DAL.Commerce;
using CommerceCore.ML;
using NuGet.Configuration;
using CommerceCore.ML.cc.sale.Home;

namespace CommerceCore.BL.cc.Home
{
    public class Welcome : LogicBase
    {
        public Welcome(Configuration settings) {
            configuration = settings;
        }

        public SaasMetricsDto GetMetricSaas(string userName)
        {
            try
            {
                using (var db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    return new SaasMetricsDto
                    {
                        TotalEmpresas = db.Empresas.Count(),
                        TotalUsuarios = db.Usuarios.Count(),
                        TotalBodegas = db.Bodegas.Count(),
                        TotalClientes = db.Clientes.Count(),
                        TotalCotizaciones = db.Cotizaciones.Count(),
                        TotalCategorias = db.Categorias.Count(),
                        TotalVendedores = db.Vendedores.Count(),
                        TotalArticulos = db.Articulos.Count(),
                        TotalExistenciaBodegas = db.ExistenciaBodegas.Count(),
                        TotalRoles = db.Rols.Count(),
                        FechaConsulta = DateTime.Now
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener métricas SaaS: {ex.Message}");
            }
        }



    }


}
