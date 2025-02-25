using CommerceCore.Api.Tools;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.BL.cc.Security;
using CommerceCore.ML.Security;
using System.Reflection;
using System.Text.Json;
using System.Security.Permissions;


namespace CommerceCore.Api.Controllers.security
{
    [Route("Security/User")]
    [ApiController]
    public class UserController : CustomController
    {
        private ServiceSecurity blServiceSecurity { get; } = new ServiceSecurity(Tool.configuration);

        /// <summary>
        /// Obtener Usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult GetUsers()
        {
            try
            {

                return Ok(blServiceSecurity.GetUsers(IdAplication));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los ususarios de la aplicacion {ex}");
            }
        }


        /// <summary>
        /// Obtener usuarios Activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("Active")]
        public IActionResult GetUsersActive()
        {
            try
            {

                return Ok(blServiceSecurity.GetUsers(IdAplication));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los ususarios de la aplicacion {ex}");
            }
        }

        /// <summary>
        /// Actualizar el estado del usuario
        /// </summary>
        /// <param name="idUser">ID del usuario a actualizar</param>
        /// <returns>Resultado de la actualización</returns>
        [HttpPut("{idUser}")]
        public IActionResult UpdateStatusUser(string idUser)
        {
            try
            {
                var result = blServiceSecurity.UpdateStatusUser(idUser);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"Error al actualizar el estado del usuario: {ex.Message}" });
            }
        }



    }
}
