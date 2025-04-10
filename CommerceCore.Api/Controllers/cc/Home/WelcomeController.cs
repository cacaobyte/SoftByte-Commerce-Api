using CommerceCore.Api.Tools;
using CommerceCore.BL.cc.sale.quotes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CommerceCore.BL.cc.Home;


namespace CommerceCore.Api.Controllers.cc.Home
{
    [Route("home")]
    [ApiController]
    public class WelcomeController : CustomController
    {
        private Welcome blwelcome = new Welcome(Tool.configuration);
        /// <summary>
        /// Devuelve los datos de metricas almacenados por el sistemas Saas
        /// </summary>
        /// <returns></returns>
        [HttpGet("general")]
        public IActionResult GetAllmetricsSaas()
        {
            try
            {
                var result = blwelcome.GetMetricSaas(userName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Devuelve los datos de metricas almacenados por el sistemas Saas de mi compañias
        /// </summary>
        /// <returns></returns>
        [HttpGet("company")]
        public IActionResult GetAllMetricsCompany()
        {
            try
            {
                //  var result = quotesBl.GetQuotesCacao(userName);
                return Ok("");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
