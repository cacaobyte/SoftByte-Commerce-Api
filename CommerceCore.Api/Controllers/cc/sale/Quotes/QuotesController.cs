using CommerceCore.Api.Tools;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.BL.cc.sale.quotes;
using CommerceCore.ML.cc.sale.Quotes;
using Microsoft.EntityFrameworkCore;


namespace CommerceCore.Api.Controllers.cc.sale.Quotes
{
    [Route("quotes")]
    [ApiController]
    public class QuotesController : CustomController
    {

        private QuotesBl quotesBl = new QuotesBl(Tool.configuration);
        /// <summary>
        /// Devuelve las regiones existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet("allQuotes")]
        public IActionResult GetAllQuotes()
        {
            try
            {
                var result = quotesBl.GetQuotes(userName, IdAplication);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Crea una nueva cotización
        /// </summary>
        /// <param name="request">Datos de la cotización</param>
        /// <returns></returns>
        [HttpPost("create")]
        public IActionResult CreateQuote([FromBody] CreateQuoteRequest request)
        {
            try
            {
                // Validar que la solicitud no sea nula y tenga detalles
                if (request == null || request.Detalles == null || request.Detalles.Count == 0)
                {
                    return BadRequest(new { message = "La solicitud es inválida o no contiene detalles de la cotización." });
                }

                // Crear la cotización
                var nuevaCotizacion = quotesBl.CreateQuote(request, userName, IdAplication);

                // Verificar si la cotización fue creada correctamente
                if (nuevaCotizacion == null || nuevaCotizacion.IdCotizacion <= 0)
                {
                    return StatusCode(500, new { message = "No se pudo generar la cotización. Intente nuevamente." });
                }

                return CreatedAtAction(nameof(GetAllQuotes), new { id = nuevaCotizacion.IdCotizacion }, nuevaCotizacion);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Error en la base de datos", error = ex.InnerException?.Message ?? ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "Error en los datos enviados", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error inesperado al crear la cotización", error = ex.Message });
            }
        }



    }
}
