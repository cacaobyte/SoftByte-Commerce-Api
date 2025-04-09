using CommerceCore.Api.Tools;
using CommerceCore.BL.cc.Rrhh;
using CommerceCore.ML;
using CommerceCore.ML.cc.Rrhh;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Api.Controllers.cc.Rrhh
{
    [Route("rrhh")]
    [ApiController]
    public class PositionsController : CustomController
    {
        private readonly Positions blPosition = new Positions(Tool.configuration);

        /// <summary>
        /// Obtener todos los puestos de la compañía
        /// </summary>
        [HttpGet("allPositions")]
        public IActionResult GetAllPositions()
        {
            try
            {
                var result = blPosition.GetPositionsCompany(userName, IdAplication);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Crear un nuevo puesto
        /// </summary>
        [HttpPost("createPosition")]
        public IActionResult CreatePosition([FromBody] PositionsRrhh request)
        {
            try
            {
                if (request == null || request.IdDepartamento == Guid.Empty)
                    return BadRequest(new { message = "Datos incompletos para la creación del puesto." });

                var result = blPosition.CreatePosition(request, userName, request.IdDepartamento, IdAplication);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Actualizar un puesto existente
        /// </summary>
        [HttpPut("updatePosition")]
        public IActionResult UpdatePosition([FromBody] Puesto request)
        {
            try
            {
                if (request == null || request.IdPuesto == Guid.Empty)
                    return BadRequest(new { message = "Datos del puesto inválidos para actualizar." });

                var result = blPosition.UpdatePosition(request, userName, IdAplication);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
