using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CC.Configurations;
using CommerceCore.Api.Attribute;
using CommerceCore.Security;
using CommerceCore.EL;
using System.IdentityModel.Tokens.Jwt;


namespace CommerceCore.Api.Tools
{
    /// <summary>
    /// Cotrolador que se usa como herencia para peticiones del commerce
    /// </summary>
    [AppKeyOnlyFilter]
    public class CommerceController : ControllerBase
    {

        private ComerceCoreInfo GetCommerceInfo()
        {
            string appKey = HttpContext.Request.Headers["AppKey"].ToString();

            if (!string.IsNullOrWhiteSpace(appKey))
            {
                // Aquí podrías traer los datos desde tu DB o configuración
                // Simulación tipo hardcoded como ejemplo:
                if (appKey == "demoKey123")
                {
                    return new ComerceCoreInfo
                    {
                        Id = 10,
                        Appkey = appKey,
                        Empresa = 2001,
                        Nombre = "Empresa Demo",
                        Activo = true,
                        Accesstoken = "token12345",
                        Cookieexpire = DateTime.UtcNow.AddHours(2),
                        Interno = false,
                        plan = "Premium"
                    };
                }

     
            }

            return null;
        }

    }
}
