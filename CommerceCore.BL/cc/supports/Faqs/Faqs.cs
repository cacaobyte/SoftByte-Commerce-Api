using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC;
using CommerceCore.DAL.Commerce;
using CommerceCore.ML;

namespace CommerceCore.BL.cc.supports.Faqs
{
    public class Faqs : LogicBase
    {

        public Faqs(Configuration settings) {
            configuration = settings;
        }


        /// <summary>
        /// Consulta las preguntas frecuentes para el apartado de soporte
        /// </summary>
        /// <returns>Devuelve las preguntas frecuentes</returns>

        public List<Faq> GetFaqs()
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    var Faq = new List<Faq>();

                    Faq = db.Faqs.Where(f => f.Estado == true).ToList();

                    return Faq;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        
        }

    }
}
