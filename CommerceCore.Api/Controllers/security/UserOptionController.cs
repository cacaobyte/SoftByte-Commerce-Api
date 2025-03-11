using CommerceCore.Api.Tools;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.BL.cc.Security;
using CommerceCore.ML.Security;
using System.Reflection;
using System.Text.Json;
using System.Security.Permissions;

namespace CommerceCore.Api.Controllers.security
{
    [Route("Security/UserOption")]
    [ApiController]
    public class UserOptionController : CustomController
    {
        private ServiceSecurity blServiceSecurity { get; } = new ServiceSecurity(Tool.configuration);


        /// <summary>
        /// Obtener usuario-opcion por id de accion
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult GetUserOptions()
        {
            try
            {
                
                return Ok(blServiceSecurity.GetUserOptions(IdAplication));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las optiones del usuario{ex}");
            }
        }


        /// <summary>
        /// Obtener usuario-opcion por id de accion
        /// </summary>
        /// <returns></returns>
        [HttpGet("{actionId}")]
        public IActionResult GetUserOptions(int actionId)
        {
            try
            {

                return Ok(blServiceSecurity.GetUserOptions(actionId));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener  usuario-opcion por id de accion{ex}");
            }
        }


        /// <summary>
        /// Crea un nuevo RolOpcionAccion de seguridad
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult CrearUsuarioOpcion([FromBody] SecurityUserOption userOptionModel)
        {
            try
            {

                return Ok(blServiceSecurity.CreateUsuarioOpcion(userOptionModel));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al Crea un nuevo RolOpcionAccion de seguridad{ex}");
            }
        }


        /// <summary>
        /// Obtener todos los usuarios que tengan asignado algun rol
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUsersWithOptions")]
        public IActionResult GetUsersWithOptions()
        {
            try
            {
             
                return Ok(blServiceSecurity.GetUsersWithOptions(IdAplication));//IdAplication));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al Obtener todos los usuarios que tengan asignado algun rol{ex}");
            }
        }

        /// <summary>
        /// Actualizar estado de Usuario-Opcion
        /// </summary>
        /// <returns></returns>
        [HttpPut()]
        public IActionResult UpdateUserOptionStatus([FromBody] SecurityUserOption userOptionModel)
        {
            try
            {

                return Ok(blServiceSecurity.UpdateUserOptionStatus(userOptionModel));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al Actualizar estado de Usuario-Opcion{ex}");
            }
        }

        [HttpGet("GetOption/{user}")]
        public IActionResult GetUserOption(string user)
        {
            try
            {

                return Ok(blServiceSecurity.GetUserOption(user));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener opciones por usuario {ex}");
            }
        }



    }
}
