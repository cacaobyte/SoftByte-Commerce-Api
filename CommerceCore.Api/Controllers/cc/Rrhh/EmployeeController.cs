using CommerceCore.Api.Tools;
using CommerceCore.BL.cc.Rrhh;
using CommerceCore.ML;
using CommerceCore.ML.cc.Rrhh;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace CommerceCore.Api.Controllers.cc.Rrhh
{
    [Route("rrhh")]
    [ApiController]
    public class EmployeeController : CustomController
    {
        private readonly Employees blEmployees = new Employees(Tool.configuration);

        /// <summary>
        /// Obtener todos los puestos de la compañía
        /// </summary>
        [HttpGet("allEmployees")]
        public IActionResult GetAllPositions()
        {
            try
            {
                var result = blEmployees.GetEmployeesCompany(userName, IdAplication);
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
        [HttpPost("createEmployee")]
        public IActionResult CreateEmployee([FromBody] EmployeesRrhh request)
        {
            try
            {
                if (request.Puesto == null || request.Puesto == Guid.Empty)
                    return BadRequest(new { message = "El ID del puesto es obligatorio." });

                var result = blEmployees.CreateEmployees(request, userName, request.Puesto.Value, IdAplication);
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
        [HttpPut("updateEmployee")]
        public IActionResult UpdatePosition([FromBody] Empleado request)
        {
            try
            {
                if (request == null || request.IdEmpleado == Guid.Empty)
                    return BadRequest(new { message = "Datos del puesto inválidos para actualizar." });

                var result = blEmployees.UpdateEmployee(request, userName, IdAplication);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
