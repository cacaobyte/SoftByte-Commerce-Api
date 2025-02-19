﻿using CommerceCore.Api.Tools;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.BL.cc.Security;
using CommerceCore.ML.Security;
using System.Reflection;
using System.Text.Json;
using System.Security.Permissions;

namespace CommerceCore.Api.Controllers.security
{
    [Route("Seguridad/Option")]
    [ApiController]
    public class OptionController : CustomController
    {
        private ServiceSecurity blServiceSecurity { get; } = new ServiceSecurity(Tool.configuration);

        /// <summary>
        /// Crea una nueva Opcion de seguridad
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult CreateNewOption([FromBody] SecurityOption optionModel)
        {
            try
            {

                return Ok(blServiceSecurity.CreateOption(optionModel));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear una nueva option: {ex}");
            }
        }


        /// <summary>
        /// Obtener aplicaciones
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult GetOptions()
        {
            try
            {

                return Ok(JsonSerializer.Serialize(blServiceSecurity.GetOptions(IdAplication)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al traer todas las opciones: {ex}");
            }
        }


        /// <summary>
        /// Obtener roles de usuario por su id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public IActionResult GetOptionsByUserId(string userId)
        {
            try
            {

                return Ok(JsonSerializer.Serialize(blServiceSecurity.GetOptionsByUserId(userId)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al traer los roles por usuario: {ex}");
            }
        }

        /// <summary>
        /// Actualizar estado de opcion
        /// </summary>
        /// <returns></returns>
        [HttpPut("{optionId}/{status}")]
        public IActionResult UpdateOptionStatus(int optionId, bool status)
        {
            try
            {

                return Ok(blServiceSecurity.UpdateOption(optionId, status));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el estado de la opcion: {ex}");
            }
        }




    }
}
