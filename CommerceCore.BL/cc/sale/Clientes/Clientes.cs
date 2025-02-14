using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC;
using NuGet.Configuration;
using CommerceCore.ML;
using CommerceCore.DAL.Commerce;

namespace CommerceCore.BL.cc.sale.Clientes
{
    public class Clientes : LogicBase
    {
        public Clientes(Configuration settings) {
            configuration = settings;
        }

        public List<Cliente> GetClient(string userName)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    var clients = new List<Cliente>();
                    clients = db.Clientes.Where(c => c.Activo == true ).ToList();
                    return clients;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        
        }
    }
}
