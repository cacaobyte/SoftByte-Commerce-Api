using CommerceCore.Api.Tools;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.BL.cc.Security;
using CommerceCore.ML.Security;
using System.Reflection;
using System.Text.Json;
using System.Security.Permissions;

namespace CommerceCore.Api.Controllers.security
{
    [Route("Security/UserOptionAction")]
    [ApiController]
    public class UserOptionActionController : CustomController
    {
        private ServiceSecurity blServiceSecurity { get; } = new ServiceSecurity(Tool.configuration);

        /// <summary>
        /// Crea un nuevo RolOpcionAccion de seguridad
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult CrearUsuarioOpcionAccion([FromBody] SecurityUserOptionAction userOptionActionModel)
        {
            try
            {

                return Ok(blServiceSecurity.CreateUserOptionAction(userOptionActionModel));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener al crear una accion al usuario accion{ex}");
            }
        }




        /// <summary>
        /// Obtener relaciones de opcion-accion 
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult GetUserOptionActions()
        {
            try
            {

                return Ok(JsonSerializer.Serialize(blServiceSecurity.GetUserOptionActions(IdAplication)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al traer las acciones del useroption {ex}");
            }
        }

        /// <summary>
        /// Actualizar estado de Usuario-Opcion
        /// </summary>
        /// <returns></returns>
        [HttpPut()]
        public IActionResult DeleteUserOptionActionStatus([FromBody] SecurityUserOptionAction userOptionActionModel)
        {
            try
            {
                return Ok(blServiceSecurity.RemoveUserOptionActionStatus(userOptionActionModel));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cambiar el estado de una accion del usuario opcion {ex}");
            }
        }

    }
}
