using CommerceCore.Api.Tools;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.BL.cc.Security;
using CommerceCore.ML.Security;
using System.Reflection;
using System.Text.Json;
using System.Security.Permissions;

namespace CommerceCore.Api.Controllers.security
{
    [Route("Seguridad/Menu")]
    [ApiController]
    public class MenuController : CustomController
    {

        private ServiceSecurity blServiceSecurity { get; } = new ServiceSecurity(Tool.configuration);

        /// <summary>
        /// Obtener menus
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult GetMenus()
        {
            try
            {
                return Ok(JsonSerializer.Serialize(blServiceSecurity.GetMenus(IdAplication)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el menu de la aplicación{ex}");
            }
        }


        /// <summary>
        /// Obtener menus
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllMenu")]
        public IActionResult GetAllMenus()
        {
            try
            {
                return Ok(JsonSerializer.Serialize(blServiceSecurity.GetAllMenus()));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el menu de la aplicación{ex}");
            }
        }







    }
}
