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
    [Route("Security/RoleUser")]
    [ApiController]
    public class RoleUserController : CustomController
    {
        private ServiceSecurity blServiceSecurity { get; } = new ServiceSecurity(Tool.configuration);


        /// <summary>
        ///Obtener todos los usuarios que tengan asignado algun rol por su id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public IActionResult GetRolByUserId(string userId)
        {
            try
            {

                return Ok(JsonSerializer.Serialize(blServiceSecurity.GetRolesByUserId(userId)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los roles por usuario {ex}");
            }
        }


        /// <summary>
        /// Obtener todos los usuarios con rol asignado
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUsersWithRoles")]
        public IActionResult GetUsersWithRoles()
        {
            try
            {

                return Ok(blServiceSecurity.GetUsersWithRole(IdAplication));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los usuarios con roles {ex}");
            }
        }


        /// <summary>
        /// Obtener todos los registros rol-usuario
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult GetRoleUsers()
        {
            try
            {

                return Ok(blServiceSecurity.GetRoleUsers(IdAplication));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los roles con usuario {ex}");
            }
        }

        /// <summary>
        /// Eliminar Rol-Usuario
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{userId}/{rolId}")]
        public IActionResult DeleteRoleUser(string userId, int rolId)
        {
            try
            {

                return Ok(blServiceSecurity.DeleteRolUser(rolId, userId));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar un roll del usuario {ex}");
            }
        }

        /// <summary>
        /// Crea un nuevo RolUsuario de seguridad
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult CreateNewRoleUser([FromBody] SecurityRoleUser rolUserModel)
        {
            try
            {

                return Ok(blServiceSecurity.CreateRoleUser(rolUserModel));
            }
            catch (HttpResponseException ex)
            {
                throw;
            }
        }




    }
}
