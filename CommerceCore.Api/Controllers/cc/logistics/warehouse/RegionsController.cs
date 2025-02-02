using CommerceCore.Api.Tools;
using CommerceCore.BL.cc.logistics;
using CommerceCore.ML;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.ML.cc.sale.Warehouse;
using CommerceCore.BL.cc.logistics.warehouse;

namespace CommerceCore.Api.Controllers.cc.logistics.warehouse
{
    [Route("api/cc/warehouse/logistic")]
    [ApiController]
    public class RegionsController : CustomController
    {
        private Regions blRegions = new Regions(Tool.configuration);


        /// <summary>
        /// Devuelve las regiones existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet("regions")]
        public IActionResult GetAllRegions()
        {
            try
            {
                var result = blRegions.GetRegions();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
