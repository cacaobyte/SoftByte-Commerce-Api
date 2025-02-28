using CommerceCore.Api.Tools;
using CommerceCore.BL.cc.Security;
using CommerceCore.ML.cc.Security.Users;
using Microsoft.AspNetCore.Identity.Data;
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
        public async Task<IActionResult> RegisterUser([FromForm] CreateUser createUser, [FromForm] IFormFile imageFile)
        {
            if (createUser == null)
                return BadRequest("El objeto CreateUser no puede ser nulo.");

            try
            {
                var newUser = await blUsers.RegisterUser(createUser, userName, imageFile, IdAplication);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }



        /// <summary>
        /// Inicia sesión y genera un token JWT
        /// </summary>
        /// <param name="loginRequests">Objeto con el nombre de usuario/correo y contraseña</param>
        /// <returns>Token JWT si la autenticación es exitosa</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginRequest loginRequests)
        {
            string user = userName;
            if (loginRequests == null || string.IsNullOrEmpty(loginRequests.UserNameOrEmail) || string.IsNullOrEmpty(loginRequests.Password))
                return BadRequest("El nombre de usuario/correo y la contraseña son obligatorios.");

            try
            {
                var token = blUsers.Login(loginRequests.UserNameOrEmail, loginRequests.Password);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene la información del usuario basado en su userName.
        /// </summary>
        /// <returns>Información del usuario</returns>
        [HttpGet("profile")]
        public IActionResult GetUserProfile()
        {
            if (string.IsNullOrEmpty(userName))
                return BadRequest("El userName es obligatorio.");

            try
            {
                var user = blUsers.GetUserByUserName(userName);

                if (user == null)
                    return NotFound(new { message = $"Usuario con userName '{userName}' no encontrado." });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


    }
}
