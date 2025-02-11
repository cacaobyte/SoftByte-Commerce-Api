using CommerceCore.Api.Tools;
using CommerceCore.BL.cc.Security;
using CommerceCore.ML.cc.Security.Users;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Api.Controllers.cc.Security
{
    [Route("security")]
    [ApiController]
    public class UsersController : CustomController
    {
        private readonly Users blUsers = new Users(Tool.configuration);



        /// <summary>
        /// Registra un nuevo usuario
        /// </summary>
        /// <param name="createUser">Objeto con la información del usuario a registrar</param>
        /// <returns>Información del usuario registrado</returns>
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] CreateUser createUser)
        {
            if (createUser == null)
                return BadRequest("El objeto CreateUser no puede ser nulo.");

            try
            {
                var newUser = blUsers.RegisterUser(createUser, userName);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
