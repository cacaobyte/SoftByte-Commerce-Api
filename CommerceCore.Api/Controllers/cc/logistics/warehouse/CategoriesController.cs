using CommerceCore.Api.Tools;
using CommerceCore.BL.cc.logistics;
using CommerceCore.ML;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.ML.cc.sale.Warehouse;
using CommerceCore.BL.cc.logistics.warehouse;

namespace CommerceCore.Api.Controllers.cc.logistics.warehouse
{


    [Route("api/cc/warehouse/logistic")]
    [ApiController]
    public class CategoriesController : CustomController
    {
    private Categories blCategories = new Categories(Tool.configuration);

        /// <summary>
        /// Devuelve las regiones existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet("categoriesActive")]
        public IActionResult GetAllCategoriesActive()
        {
            try
            {
                var result = blCategories.GetListCategoriesActive(userName, IdAplication);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Devuelve las regiones existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet("categories")]
        public IActionResult GetAllCategories()
        {
            try
            {
                var result = blCategories.GetListCategories(userName, IdAplication);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Devuelve las regiones existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet("categoriesSubCategories")]
        public IActionResult GetAllCategoriesSubCategories()
        {
            try
            {
                var result = blCategories.GetListCategoriesSubCategories(IdAplication);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza una categoría existente
        /// </summary>
        /// <param name="categoriaEdit">Objeto con los datos de la categoría a actualizar</param>
        /// <returns>Respuesta HTTP indicando el éxito o el error</returns>
        [HttpPut("categoriesUpdate")]
        public IActionResult UpdateCategory([FromBody] CategoriaEditDto categoriaEdit)
        {
            if (categoriaEdit == null || categoriaEdit.IdCategoria <= 0)
            {
                return BadRequest("Datos de categoría inválidos.");
            }

            try
            {
                var result = blCategories.UpdateCategory(categoriaEdit, userName);
                if (result)
                {
                    return Ok(new { message = "Categoría actualizada con éxito.", categoria = categoriaEdit });
                }
                else
                {
                    return StatusCode(500, "No se pudo actualizar la categoría.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Crea una nueva categoría
        /// </summary>
        /// <param name="categoriaCreate">Objeto con los datos de la nueva categoría</param>
        /// <returns>Respuesta HTTP indicando el éxito o el error</returns>
        [HttpPost("createCategory")]
        public IActionResult CreateCategory([FromBody] CategoriaCreateDto categoriaCreate)
        {
            if (categoriaCreate == null || string.IsNullOrWhiteSpace(categoriaCreate.Nombre))
            {
                return BadRequest("Datos de categoría inválidos. El campo 'Nombre' es obligatorio.");
            }

            try
            {
                var result = blCategories.CreateCategory(categoriaCreate, userName, IdAplication);
                if (result)
                {
                    return Ok(new { message = "Categoría creada con éxito.", categoria = categoriaCreate });
                }
                else
                {
                    return StatusCode(500, "No se pudo crear la categoría.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la categoría: {ex.Message}");
            }
        }


        /// <summary>
        /// Alterna el estado de una categoría (activa/inactiva)
        /// </summary>
        /// <param name="idCategoria">ID de la categoría</param>
        /// <returns>Estado actualizado de la categoría</returns>
        [HttpPut("categories/toggleStatus/{idCategoria}")]
        public IActionResult ToggleCategoryStatus(int idCategoria)
        {
            try
            {
                var result = blCategories.ToggleCategoryStatus(idCategoria, userName);
                return Ok(new { idCategoria, nuevoEstatus = result });
            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message);
            }
        }


    }
}
