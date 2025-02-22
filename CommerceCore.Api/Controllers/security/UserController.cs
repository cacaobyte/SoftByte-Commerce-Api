﻿using CommerceCore.Api.Tools;
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
        /// Obtener roles
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult GetUsers()
        {
            try
            {

                return Ok(JsonSerializer.Serialize(blServiceSecurity.GetUsers(IdAplication)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los ususarios de la aplicacion {ex}");
            }
        }
    }
}
