using CommerceCore.Api.Tools;
using CommerceCore.BL.cc.Home;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Api.Controllers.cc.UpTimeRobot
{
    [Route("ping")]
    [ApiController]
    public class UpTimeRobotController : CustomController
    {

        /// <summary>
        /// Devuelve pong para mantener el servicio activo en render.com
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Ping()
        {
            try
            {

                return Ok(new { status = "pong", timestamp = DateTime.UtcNow });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
