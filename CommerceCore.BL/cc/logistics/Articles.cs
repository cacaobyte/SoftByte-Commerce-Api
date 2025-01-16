using System.Collections.Generic;
using System.Linq;
using CC;
using CommerceCore.DAL.Commerce;
using CommerceCore.DAL.Commerce.Models;
using CommerceCore.DAL.Commerce.Models.SoftByteCommerce;

namespace CommerceCore.BL.cc.logistics
{
    public class Articles : LogicBase
    {
        public Articles(Configuration settings)
        {
            configuration = settings;
        }

        public List<Articulo> GetArticulos()
        {
            using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
            {
                // Devuelve los artículos desde la base de datos
                return db.Articulos.ToList();
            }
        }
    }
}
