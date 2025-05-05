using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CC.Configurations;
using CommerceCore.Api.Attribute;
using CommerceCore.Security;
using CommerceCore.EL;
using System.IdentityModel.Tokens.Jwt;
using CommerceCore.BL.cc.Commerce;
using CommerceCore.BL.cc.Security;
using CommerceCore.ML;


namespace CommerceCore.Api.Tools
{
    /// <summary>
    /// Cotrolador que se usa como herencia para peticiones del commerce
    /// </summary>
    [AppKeyOnlyFilter]
    public class CommerceController : ControllerBase
    {
        private Aplication blServiceSecurity { get; } = new Aplication(Tool.configuration);
        private ComerceCoreInfo GetCommerceInfo()
        {
            string appKey = HttpContext.Request.Headers["AppKey"].ToString();

            if (!string.IsNullOrWhiteSpace(appKey))
            {
                var result = blServiceSecurity.GetAplication(appKey).FirstOrDefault();

                if (result != null)
                {
                    return new ComerceCoreInfo
                    {
                        Id = result.Id,
                        Appkey = result.Appkey,
                        Empresa = result.Empresa,
                        Nombre = result.Nombre,
                        Activo = result.Activo,
                        Accesstoken = result.Accesstoken,
                        Cookieexpire = result.Cookieexpire,
                        Interno = result.Interno,
                        plan = result.plan,
                        Aplicacionconfiguracion = result.Aplicacionconfiguracion,
                        Aplicacionhosts = result.Aplicacionhosts?.ToList() ?? new List<Aplicacionhost>(),
                        Menus = result.Menus?.ToList() ?? new List<Menu>(),
                        Rols = result.Rols?.ToList() ?? new List<Rol>()
                    };
                }
            }

            return null;
        }

        public int AppId => GetCommerceInfo()?.Id ?? 0;

        public string Appkey => GetCommerceInfo()?.Appkey ?? string.Empty;

        public int EmpresaId => GetCommerceInfo()?.Empresa ?? 0;

        public string EmpresaNombre => GetCommerceInfo()?.Nombre ?? "Desconocida";

        public bool EstaActiva => GetCommerceInfo()?.Activo ?? false;

        public string Accesstoken => GetCommerceInfo()?.Accesstoken ?? string.Empty;

        public DateTime? CookieExpire => GetCommerceInfo()?.Cookieexpire;

        public bool EsInterno => GetCommerceInfo()?.Interno ?? false;

        public string Plan => GetCommerceInfo()?.plan ?? "Commerce";


    }
}
