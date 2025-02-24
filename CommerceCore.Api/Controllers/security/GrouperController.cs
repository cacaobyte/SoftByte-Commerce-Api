using CommerceCore.Api.Tools;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.BL.cc.Security;
using CommerceCore.ML.Security;
using System.Reflection;
using System.Text.Json;
using System.Security.Permissions;
using CommerceCore.EL;

namespace CommerceCore.Api.Controllers.security
{
    [Route("security/grouper")]
    [ApiController]
    public class GrouperController : CustomController
    {
        private ServiceSecurity blServiceSecurity { get; } = new ServiceSecurity(Tool.configuration);

        /// <summary>
        /// Obtener agrupadores del menu
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult GetGroupers()
        {
            try
            {
                return Ok(blServiceSecurity.GetGroupers(Plan));
            }
            catch (HttpResponseException  ex)
            {
                throw new Exception($"Error al obtener la aplicaciones {ex}");
            }
        }
        /// <summary>
        /// Obtener agrupadores activos del menu
        /// </summary>
        /// <returns></returns>
        [HttpGet("Active")]
        public IActionResult GetGroupersActive()
        {
            try
            {
                return Ok(blServiceSecurity.GetGroupersActive(Plan));
            }
            catch (HttpResponseException ex)
            {
                throw new Exception($"Error al obtener la aplicaciones {ex}");
            }
        }

    }
}
