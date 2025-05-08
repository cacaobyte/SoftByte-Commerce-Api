using CommerceCore.Api.Tools;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.BL.cc.logistics;
using CommerceCore.DAL.Commerce.Models.SoftByteCommerce;
using CommerceCore.ML.cc.sale;
using CommerceCore.BL.cc.Commerce;

namespace CommerceCore.Api.Controllers.Commerce.Products
{
    [Route("commerce/products")]
    public class ProductoCommerceController : CommerceController
    {
        /// <summary>
        /// Bl para manejar operaciones de productos
        /// </summary>
        /// <returns></returns>
        private Articles blArticles { get; } = new Articles(Tool.configuration);

        /// <summary>
        /// Devuelve las existencias de artículos para tienda y clientes minuristas
        /// </summary>
        /// <returns></returns>
        [HttpGet("allProducts")]
        public IActionResult GetAllArticles()
        {
            try
            {
                var result = blArticles.GetProductosCommerce(AppId);
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
        [HttpGet("test")]
        public IActionResult GetTest()
        {
            try
            {
               
                return Ok("hello Wolrd");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the articles.{ex}");
            }
        }


    }
}
