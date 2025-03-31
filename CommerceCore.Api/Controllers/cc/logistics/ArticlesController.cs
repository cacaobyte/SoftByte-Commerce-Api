using CommerceCore.Api.Tools;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.BL.cc.logistics;
using CommerceCore.DAL.Commerce.Models.SoftByteCommerce;
using CommerceCore.ML.cc.sale;

namespace CommerceCore.Api.Controllers.cc.logistics
{
    [Route("api/cc/warehouse")]
    [ApiController] 
    public class ArticlesController : CustomController
    {
        /// <summary>
        /// Bl para manejar operaciones de articulos
        /// </summary>
        /// <returns></returns>
        private Articles blArticles { get; } = new Articles(Tool.configuration);

        /// <summary>
        /// Devuelve las existencias de artículos para tienda y clientes minuristas
        /// </summary>
        /// <returns></returns>
        [HttpGet("articulosTodos")]
        public IActionResult GetAllArticles()
        {
            try
            {
                var result = blArticles.GetArticles(IdAplication);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the articles.{ex}");
            }
        }

        /// <summary>
        /// Devuelve las existencias de artículos para tienda y clientes minuristas
        /// </summary>
        /// <returns></returns>
        [HttpGet("articulos")] 
        public IActionResult GetArticlesWarehouse()
        {
            try
            {
               
                var result = blArticles.GetArticlesWarehouse(storeCode);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the articles.{ex}");
            }
        }

        /// <summary>
        /// Devuelve las existencias de artículos para tienda y clientes minuristas
        /// <param name="warehouseSelected">Bodega selecionada</param>
        /// </summary>
        /// <returns></returns>
        [HttpGet("articlesWarehouse/{warehouseSelected}")]
        public IActionResult GetArticlesSelectedWarehouse(string warehouseSelected)
        {
            try
            {

                var result = blArticles.GetArticlesSelectedWarehouse(warehouseSelected);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the articles.{ex}");
            }
        }

        /// <summary>
        /// Devuelve las existencias de artículos para mayoreo
        /// </summary>
        /// <returns></returns>
        [HttpGet("articulosMayoreo")] 
        public IActionResult GetWholesaleItems()
        {
            try
            {
                var result = blArticles.GetWholesaleItems(IdAplication);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the articles.{ex}");
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
                var result = blArticles.GetAllWarehouseStocks(IdAplication);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An error occurred while retrieving stock items warehouse.{ex}");
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
                var result = blArticles.GetAllWarehouse(IdAplication);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Devuelve todas la bodegas
        /// </summary>
        /// <returns></returns>
        [HttpGet("bodegas/active")]
        public IActionResult GetAllWarehouseActive()
        {
            try
            {
                var result = blArticles.GetAllWarehouse(IdAplication);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        [HttpPost("crearArticulos")]
        public async Task<IActionResult> CreateArticle([FromForm] CreateArticle newArticleData, [FromForm] IFormFile imageFile)
        {
            try
            {
                if (newArticleData == null)
                {
                    return BadRequest("The article data is required.");
                }

                var createdArticle = await blArticles.CreateArticleAsync(newArticleData, imageFile, userName, IdAplication);
                return Ok(createdArticle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el artículo: {ex.Message}");
            }
        }






    }
}
