using NuGet.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC;
using CommerceCore.DAL.Commerce;
using CommerceCore.ML;

namespace CommerceCore.BL.cc.supports.GuiasSupport
{
    public class Guiass : LogicBase
    {
        public Guiass(Configuration settings) {
            configuration = settings;

        }

        public List<Guia> GetGuias()
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    var guia = new List<Guia>();

                    guia = db.Guias.Where(g => g != null && g.Estado == true).ToList();

                    return guia;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener guías: " + ex.Message, ex);
            }
        }

    }
}
