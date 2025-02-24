using System;
using System.Net;

namespace CommerceCore.EL
{
    public static class ResponseException
    {
        /// <summary>
        /// Lanza una excepción HTTP personalizada con código de estado y mensaje.
        /// </summary>
        public static void ThrowHttpResponseException(this HttpStatusCode statusCode, string message)
        {
            throw new HttpResponseException((int)statusCode, message);
        }
    }

    /// <summary>
    /// Clase de excepción personalizada para respuestas HTTP.
    /// </summary>
    public class HttpResponseException : Exception
    {
        public int StatusCode { get; }

        public HttpResponseException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
