using CommerceCore.Api.Tools;
using CommerceCore.BL.cc.logistics.warehouse;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.BL.cc.sale.Clientes;

namespace CommerceCore.Api.Controllers.cc.sale.Client
{
    [Route("clients")]
    [ApiController]
    public class ClientController : CustomController
    {
        private Clientes client = new Clientes(Tool.configuration);

        /// <summary>
        /// Devuelve las regiones existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet("allClients")]
        public IActionResult GetAllRegions()
        {
            try
            {
                var result = client.GetClient(userName, IdAplication);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
