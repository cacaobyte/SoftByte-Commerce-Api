﻿using CommerceCore.Api.Tools;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.BL.cc.logistics;
using CommerceCore.DAL.Commerce.Models.SoftByteCommerce;
using CommerceCore.ML.cc.sale;

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
        /// Devuelve las existencias de artículos para tienda y clientes minuristas
        /// </summary>
        /// <returns></returns>
        [HttpGet("articulosTodos")]
        public IActionResult GetAllArticles()
        {
            try
            {
                var result = blArticles.GetArticles();
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
                string warehouse = "B001";
                var result = blArticles.GetArticlesWarehouse(warehouse);
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
                var result = blArticles.GetWholesaleItems();
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
                var result = blArticles.GetAllWarehouseStocks();
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
                var result = blArticles.GetAllWarehouse();
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An error occurred while retrieving all warehouse.{ex}");
            }
        }


        [HttpPost("crearArticulos")]
        public IActionResult CreateArticle([FromForm] CreateArticle newArticleData, [FromForm] IFormFile imageFile)
        {
            try
            {
                if (newArticleData == null)
                {
                    return BadRequest("The article data is required.");
                }

                var createdArticle = blArticles.CreateArticle(newArticleData, imageFile, userName);
                return Ok(createdArticle);
            }
            catch (Exception ex)
            {
                throw new Exception( ex.Message );
            }
        }





    }
}
