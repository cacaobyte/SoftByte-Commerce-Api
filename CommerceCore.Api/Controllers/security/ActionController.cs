using CommerceCore.Api.Tools;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.BL.cc.Security;
using CommerceCore.ML.Security;
using System.Reflection;
using System.Text.Json;
using System.Security.Permissions;

namespace CommerceCore.Api.Controllers.security
{
    [Route("security/action")]
    [ApiController]
    public class ActionController : CustomController
    {
        private ServiceSecurity blServiceSecurity  { get; } = new ServiceSecurity(Tool.configuration);

        /// <summary>
        /// Obtener acciones
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetActions()
        {
            try
            {

                return Ok(JsonSerializer.Serialize(blServiceSecurity.GetActions(IdAplication)));
            }
            catch (Exception ex)
            {

                throw new Exception($"Error al obtener las action{ex}");
            }
        }



        /// <summary>
        /// Actualizar estado de accion
        /// </summary>
        /// <param name="actionId">Fecha inicial del periodo</param>
        /// <param name="status">Fecha fina periodo</param>
        /// <returns></returns>
        [HttpPut("{actionId}/{status}")]
        public IActionResult UpdateActionStatus(int actionId, bool status)
        {
            try
            {
                return Ok(blServiceSecurity.UpdateAction(actionId, status));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar la action: {ex}");
            }
        }


        /// <summary>
        /// Crea nueva accion de seguridad
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult CrearAccion([FromBody] SecurityActions actionModel)
        {
            try
            {

                return Ok(blServiceSecurity.CreateAction(actionModel, IdAplication));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear la  action: {ex}");
            }
        }




    }
}
