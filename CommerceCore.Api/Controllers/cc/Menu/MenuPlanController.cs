using CommerceCore.Api.Tools;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.BL.cc.Menu;
using CommerceCore.BL.cc.sale.quotes;

namespace CommerceCore.Api.Controllers.cc.Menu
{
    [Route("menu")]
    [ApiController]
    public class MenuPlanController : CustomController
    {

        private MenuPlan menuBl = new MenuPlan(Tool.configuration);

        /// <summary>
        /// Devuelve el menu del plan
        /// </summary>
        /// <returns></returns>
        [HttpGet("plan")]
        public IActionResult GetAllQuotesCacaoByte()
        {
            try
            {
                var result = menuBl.GetMenuPlan(IdAplication, Plan);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
