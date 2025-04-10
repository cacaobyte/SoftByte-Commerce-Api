using CommerceCore.Api.Tools;
using CommerceCore.BL.cc.Rrhh;
using CommerceCore.ML;
using CommerceCore.ML.cc.Rrhh;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Api.Controllers.cc.Rrhh
{
    [Route("rrhh")]
    [ApiController]
    public class RrhhController : CustomController
    {
        private readonly Departaments departamentsBl = new Departaments(Tool.configuration);

        /// <summary>
        /// Devuelve los departamentos de la empresa
        /// </summary>
        [HttpGet("allDepartaments")]
        public IActionResult GetAllDepartaments()
        {
            try
            {
                var result = departamentsBl.GetDepartamentsCompany(userName, IdAplication);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Crea un nuevo departamento
        /// </summary>
        [HttpPost("createDepartament")]
        public IActionResult CreateDepartament([FromBody] DepartamentsRrhh request)
        {
            try
            {
                var result = departamentsBl.CreateDepartamentsCompany(request, userName, IdAplication);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza un departamento existente
        /// </summary>
        [HttpPut("updateDepartament")]
        public IActionResult UpdateDepartament([FromBody] Departamento request)
        {
            try
            {
                var result = departamentsBl.UpdateDepartamentsCompany(request, userName, IdAplication);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
