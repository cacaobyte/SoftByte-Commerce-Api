using Microsoft.AspNetCore.Mvc.Filters;

namespace CommerceCore.Api.Attribute
{
    public class MiddlewareResponse : ResultFilterAttribute, IResultFilter
    {
        /// <summary>
        /// Método en donde se agregan las cabeceras
        /// </summary>
        /// <param name="filter">Contexto de la aplicación</param>
        public override void OnResultExecuting(ResultExecutingContext filter)
        {
            string token = filter.HttpContext.Request.Headers["Token"].ToString(),
                appKey = filter.HttpContext.Request.Headers["AppKey"].ToString();

          /*  if (!token.IsNull())
            {
               // string renewedToken = blSecurity.RenewToken(token, appKey);
                filter.HttpContext.Response.Headers.Add("RenewedToken");//, renewedToken
            }
          */
            base.OnResultExecuting(filter);
        }
    }
}
