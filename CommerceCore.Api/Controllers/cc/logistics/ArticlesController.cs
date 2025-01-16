using CommerceCore.Api.Tools;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.BL.cc.logistics;

namespace CommerceCore.Api.Controllers.cc.logistics
{
    [Route("api/cc/warehouse")]
    [ApiController] // Recomendado para habilitar características automáticas.
    public class ArticlesController : CustomController
    {
        private Articles blArticles { get; } = new Articles(Tool.configuration);
        /// <summary>
        /// Devuelve las existencias de articulos
        /// </summary>
        /// <returns></returns>
        [HttpGet("articulos")] // Usa una ruta relativa para que se combine con la ruta base del controlador.
        public IActionResult GetArticlesWarehouse()
        {
            var resultado = blArticles.GetArticulos();
            return Ok(resultado);
        }
    }
}
