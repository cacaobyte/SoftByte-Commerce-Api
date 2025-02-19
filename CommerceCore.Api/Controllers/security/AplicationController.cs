using CommerceCore.Api.Tools;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.BL.cc.Security;
using CommerceCore.ML.Security;
using System.Reflection;
using System.Text.Json;
using System.Security.Permissions;

namespace CommerceCore.Api.Controllers.security
{
    [Route("Seguridad/Apps")]
    [ApiController]
    public class AplicationController : CustomController
    {

        private ServiceSecurity blServiceSecurity { get; } = new ServiceSecurity(Tool.configuration);

        /// <summary>
        /// Obtener aplicaciones
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult GetAplications()
        {
            try
            {
                return Ok(JsonSerializer.Serialize(blServiceSecurity.GetAplications()));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la aplicaciones {ex}");
            }
        }

    }
}
