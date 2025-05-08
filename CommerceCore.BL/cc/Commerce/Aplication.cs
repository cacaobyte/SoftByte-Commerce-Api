using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC;
using NuGet.Configuration;
using CommerceCore.ML;
using CommerceCore.DAL.Commerce;
using CommerceCore.BL.cc.cloudinary;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using CommerceCore.ML.cc.sale.Clients;

namespace CommerceCore.BL.cc.Commerce
{
    public class Aplication : LogicBase
    {
        public Aplication(Configuration settings) {
            configuration = settings;
        }
        public List<Aplicacion> GetAplication( string appKey)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    var aplication = new List<Aplicacion>();
                    aplication = db.Aplicacions.Where(c => c.Activo == true && c.Appkey == appKey).ToList();
                    return aplication;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
