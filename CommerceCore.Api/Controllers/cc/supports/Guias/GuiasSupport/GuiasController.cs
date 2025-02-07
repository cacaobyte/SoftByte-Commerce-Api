using CommerceCore.Api.Tools;
using CommerceCore.BL.cc.supports.GuiasSupport;
using CommerceCore.ML;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Api.Controllers.cc.supports.Guias.GuiasSupport
{
    [Route("guia")]
    [ApiController]
    public class GuiasController : CustomController
    {
        /// <summary>
        /// Bl para manejar preguntas frecuentes
        /// </summary>
        /// <returns></returns>
        private Guiass blGuias { get; } = new Guiass(Tool.configuration);

        /// <summary>
        /// Devuelve todas las preguntas frecuentes
        /// </summary>
        /// <returns></returns>
        [HttpGet("allGuia")]
        public IActionResult GetAllGuias()
        {
            try
            {
                var result = blGuias.GetGuias();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the articles.{ex}");
            }
        }
    }
}
