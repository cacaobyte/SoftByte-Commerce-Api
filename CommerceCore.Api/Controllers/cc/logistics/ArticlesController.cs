using CommerceCore.Api.Tools;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.BL.cc.logistics;

namespace CommerceCore.Api.Controllers.cc.logistics
{
    [Route("api/cc/warehouse")]
    [ApiController] // Recomendado para habilitar características automáticas.
    public class ArticlesController : CustomController
    {
        /// <summary>
        /// Bl para manejar operaciones de articulos
        /// </summary>
        /// <returns></returns>
        private Articles blArticles { get; } = new Articles(Tool.configuration);

        /// <summary>
        /// Devuelve las existencias de artículos
        /// </summary>
        /// <returns></returns>
        [HttpGet("articulos")] // Usa una ruta relativa para que se combine con la ruta base del controlador.
        public IActionResult GetArticlesWarehouse()
        {
            try
            {
                var result = blArticles.GetArticles();
                return Ok(result);
            }
            catch (Exception ex)
            {

                // Devolver un error genérico al cliente, sin exponer información sensible
                return StatusCode(500, "An error occurred while retrieving the articles.");
            }
        }

        /// <summary>
        /// Devuelve las existencias de artículos
        /// </summary>
        /// <returns></returns>
        [HttpGet("existenciaBodega")] // Usa una ruta relativa para que se combine con la ruta base del controlador.
        public IActionResult GetStockItemsWarehouse()
        {
            try
            {
                var result = blArticles.GetAllWarehouseStocks();
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while retrieving stock items warehouse.");
            }
        }



        /// <summary>
        /// Devuelve todas la bodegas
        /// </summary>
        /// <returns></returns>
        [HttpGet("bodegas")] 
        public IActionResult GetAllWarehouse()
        {
            try
            {
                var result = blArticles.GetAllWarehouse();
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while retrieving all warehouse.");
            }
        }


    }
}
