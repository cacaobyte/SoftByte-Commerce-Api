using CommerceCore.Api.Tools;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.BL.cc.Security;
using CommerceCore.ML.Security;
using System.Reflection;
using System.Text.Json;
using System.Security.Permissions;

namespace CommerceCore.Api.Controllers.security
{
    [Route("Security/Roll")]
    [ApiController]
    public class RoleController : CustomController
    {
        private ServiceSecurity blServiceSecurity { get; } = new ServiceSecurity(Tool.configuration);

        /// <summary>
        /// Obtener roles
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult GetRoles()
        {
            try
            {

                return Ok(blServiceSecurity.GetRoles(IdAplication));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las action{ex}");
            }
        }


        /// <summary>
        /// Crea un nuevo Rol de seguridad
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult CreateNewRol([FromBody] SecurityRole rolModel)
        {
            try
            {

                return Ok(blServiceSecurity.CreateRole(rolModel, IdAplication));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear un nuevo roll{ex}");
            }
        }

        /// <summary>
        /// Actualizar estado de rol
        /// </summary>
        /// <returns></returns>
        [HttpPut("{roleId}/{status}")]
        public IActionResult UpdateRoleStatus(int roleId, bool status)
        {
            try
            {
                return Ok(blServiceSecurity.UpdateRole(roleId, status));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar estado del roll {ex}");
            }
        }



    }
}
