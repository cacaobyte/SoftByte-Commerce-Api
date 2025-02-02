using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CC;
using Microsoft.AspNetCore.Http;
using CommerceCore.ML;
using CommerceCore.BL.cc.cloudinary;
using CommerceCore.DAL.Commerce;

namespace CommerceCore.BL.cc.logistics.warehouse
{
    public class Regions : LogicBase
    {

        public Regions(Configuration settings)
        {
            configuration = settings;
        }

        public List<Regione> GetRegions()
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    var regions = new List<Regione>();

                    regions = db.Regiones.ToList();
                    return regions;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
