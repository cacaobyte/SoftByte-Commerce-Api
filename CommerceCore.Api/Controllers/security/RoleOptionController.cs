using CommerceCore.Api.Tools;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.BL.cc.Security;
using CommerceCore.ML.Security;
using System.Reflection;
using System.Text.Json;
using System.Security.Permissions;


namespace CommerceCore.Api.Controllers.security
{
    /// <summary>
    /// Controlador que gestiona rol-opcion
    /// </summary>
    [Route("Security/RoleOption")]
    public class RoleOptionController : CustomController
    {

        private ServiceSecurity blServiceSecurity { get; } = new ServiceSecurity(Tool.configuration);


        /// <summary>
        /// Obtener relaciones de rol-opcion
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult GetRoleOption()
        {
            try
            {
                int app = 1;
                return Ok(blServiceSecurity.GetRoleOptions(app));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los roles con opciones {ex}");
            }
        }

        /// <summary>
        /// Crea un nuevo RolOpcion de seguridad
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult CrearRolOpcion([FromBody] SecurityRoleOption rolOptionModel)
        {
            try
            {

                return Ok(blServiceSecurity.CreateRoleOption(rolOptionModel));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear rollopcion {ex}");
            }
        }


        /// <summary>
        /// Eliminar Rol-Opcion
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{rolOptionId}")]
        public IActionResult DeleteRoleOption(int rolOptionId)
        {
            try
            {
                return Ok(blServiceSecurity.DeleteRoleOption(rolOptionId));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar un roloption {ex}");
            }
        }



    }
}
