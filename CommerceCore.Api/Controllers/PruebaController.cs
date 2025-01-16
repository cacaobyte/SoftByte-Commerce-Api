using CommerceCore.Api.Tools;
using Microsoft.AspNetCore.Mvc;

namespace CommerceCore.Api.Controllers
{
    [Route("api/cc/prueba")]
    public class PruebaController : CustomController
    {
        [HttpGet]
        public IActionResult PruebaControladores()
        {
            return Ok("Prueba del controladores exitosa");
        }
    }
}
