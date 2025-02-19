using CommerceCore.Api.Tools;
using CommerceCore.BL.cc.logistics;
using CommerceCore.ML;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.ML.cc.sale.Warehouse;

namespace CommerceCore.Api.Controllers.cc.logistics.warehouse
{
    [Route("api/cc/warehouse/logistic")]
    [ApiController]
    public class warehouseController : CustomController
    {
        private Warehouse blWarehouse = new Warehouse(Tool.configuration);

        /// <summary>
        /// Agrega una nueva bodega al sistema generando automáticamente el ID.
        /// </summary>
        /// <param name="newWarehouse">Objeto con los datos de la bodega</param>
        /// <returns>Mensaje de éxito o error</returns>
        [HttpPost("addWarehouse")]
        public IActionResult AddWarehouse([FromBody] CreateWarehouse newWarehouse)
        {
            try
            {
                if (newWarehouse == null)
                {
                    return BadRequest(new { message = "Los datos de la bodega son inválidos." });
                }

                var resultado = blWarehouse.AddWarehouse(newWarehouse, userName, IdAplication);

                if (resultado.StartsWith("Error"))
                {
                    return Conflict(new { message = resultado });
                }

                return Ok(new { message = resultado });
            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Actualiza los datos de una bodega existente.
        /// </summary>
        /// <param name="warehouseId">ID de la bodega a actualizar</param>
        /// <param name="updatedWarehouse">Objeto con los datos actualizados de la bodega</param>
        /// <returns>Mensaje de éxito o error</returns>
        [HttpPut("editWarehouse/{warehouseId}")]
        public IActionResult UpdateWarehouse(string warehouseId, [FromBody] CreateWarehouse updatedWarehouse)
        {
            try
            {
                if (updatedWarehouse == null)
                {
                    return BadRequest(new { message = "Los datos de la bodega son inválidos." });
                }

                var resultado = blWarehouse.UpdateWarehouse(warehouseId, updatedWarehouse, userName);

                if (resultado.StartsWith("Error"))
                {
                    return NotFound(new { message = resultado });
                }

                return Ok(new { message = resultado });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Activa o desactiva una bodega por su ID.
        /// </summary>
        /// <param name="warehouseId">El ID de la bodega a modificar</param>
        /// <returns>Mensaje indicando si la bodega fue activada o desactivada</returns>
        [HttpPut("updateWarehouse/{warehouseId}")]
        public IActionResult UpdateStatusWarehouse(string warehouseId)
        {
            try
            {
                var resultado = blWarehouse.UpdateStatusWarehouse(warehouseId);

                if (string.IsNullOrEmpty(resultado))
                {
                    return NotFound(new { message = "Bodega no encontrada" });
                }

                return Ok(new { message = resultado });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
