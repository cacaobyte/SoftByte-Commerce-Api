using CommerceCore.Api.Tools;
using CommerceCore.BL.cc.logistics;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.BL.cc.supports.Faqs;

namespace CommerceCore.Api.Controllers.cc.supports.FaqsSupport
{
    [Route("faqs")]
    [ApiController]
    public class FaqsController : CustomController
    {
        /// <summary>
        /// Bl para manejar preguntas frecuentes
        /// </summary>
        /// <returns></returns>
        private Faqs blFaqs { get; } = new Faqs(Tool.configuration);

        /// <summary>
        /// Devuelve todas las preguntas frecuentes
        /// </summary>
        /// <returns></returns>
        [HttpGet("allFaqs")]
        public IActionResult GetAllFaqs()
        {
            try
            {
                  var result = blFaqs.GetFaqs();
          
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the articles.{ex}");
            }
        }
    }
}
