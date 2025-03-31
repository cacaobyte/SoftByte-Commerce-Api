using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC;
using NuGet.Configuration;
using CommerceCore.ML;
using CommerceCore.DAL.Commerce;
using CommerceCore.BL.cc.cloudinary;
using Microsoft.AspNetCore.Http;
using CommerceCore.BL.cc.cloudinary;
using Microsoft.EntityFrameworkCore;
using CommerceCore.ML.cc.sale.Quotes;

namespace CommerceCore.BL.cc.sale.quotes
{
    public class QuotesBl : LogicBase
    {
        public QuotesBl(Configuration settings) {
            configuration = settings;
        }

        public List<Cotizacione> GetQuotesCacao(string userName)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    var quotes = new List<Cotizacione>();
                    quotes = db.Cotizaciones.ToList();
                    return quotes;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Agrega una nueva cotización.
        /// </summary>
        /// <param name="request">Objeto con los datos de la cotización</param>
        /// <param name="userName">Usuario que crea la cotización</param>
        /// <param name="aplication">Id de la aplicacion</param>
        /// <returns>Mensaje de éxito o error</returns>
        public Cotizacione CreateQuote(CreateQuoteRequest request, string userName, int aplication)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    if (request.Detalles == null || !request.Detalles.Any())
                    {
                        throw new Exception("La cotización debe contener al menos un artículo.");
                    }

                    // Convertir fechas para PostgreSQL
                    DateTime fechaActual = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

                    var newQuote = new Cotizacione
                    {
                        FechaCreacion = fechaActual,
                        FechaActualizacion = fechaActual,
                        ClienteId = request.ClienteId,
                        NombreCliente = request.NombreCliente,
                        ApellidoCliente = request.ApellidoCliente,
                        Correo = request.Correo,
                        Telefono = request.Telefono,
                        TipoPago = request.TipoPago,
                        DescuentoCliente = request.DescuentoCliente,
                        Subtotal = request.Subtotal,
                        DescuentoTotal = request.DescuentoTotal,
                        Impuestos = request.Impuestos,
                        Total = request.Total,
                        Estado = request.Estado,
                        Moneda = request.Moneda,
                        Origen = request.Origen,
                        UsuarioCreador = userName,
                        Notas = request.Notas,
                        aplicacion = aplication
                    };

                    db.Cotizaciones.Add(newQuote);
                    db.SaveChanges();

                    if (newQuote.IdCotizacion <= 0)
                    {
                        throw new Exception("No se pudo generar el ID de la cotización.");
                    }

                    var quoteDetails = request.Detalles.Select(detalle => new CotizacionDetalle
                    {
                        IdCotizacion = newQuote.IdCotizacion,
                        IdArticulo = detalle.IdArticulo,
                        NombreArticulo = detalle.NombreArticulo,
                        PrecioUnitario = detalle.PrecioUnitario,
                        Cantidad = detalle.Cantidad,
                        DescuentoAplicado = detalle.DescuentoAplicado,
                        Subtotal = detalle.Subtotal,
                        Impuestos = detalle.Impuestos,
                        Total = detalle.Total,
                        FechaCreacion = fechaActual,
                        FechaActualizacion = fechaActual,
                        UsuarioCreador = userName,
                        aplicacion = aplication
                    }).ToList();

                    db.CotizacionDetalles.AddRange(quoteDetails);
                    db.SaveChanges();

                    return newQuote;
                }
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Error al guardar en la base de datos: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado: {ex.Message}");
            }
        }


        /// <summary>
        /// Consulta los datos de la cotizaciones por aplicacion.
        /// </summary>
        /// <returns>Mensaje de éxito o error</returns>
        public List<QuoteResponse> GetQuotes(int aplication)
        {
            using (var db = new SoftByte(configuration.appSettings.cadenaSql))
            {
                var cotizaciones = db.Cotizaciones
                    .Where(c => c.aplicacion == aplication)
                    .ToList();

                var detalles = db.CotizacionDetalles
                    .Where(d => d.aplicacion == aplication)
                    .ToList();

                return cotizaciones.Select(cot => new QuoteResponse
                {
                    IdCotizacion = cot.IdCotizacion,
                    FechaCreacion = cot.FechaCreacion,
                    FechaActualizacion = cot.FechaActualizacion,
                    ClienteId = cot.ClienteId,
                    NombreCliente = cot.NombreCliente,
                    ApellidoCliente = cot.ApellidoCliente,
                    Correo = cot.Correo,
                    Telefono = cot.Telefono,
                    TipoPago = cot.TipoPago,
                    DescuentoCliente = cot.DescuentoCliente,
                    Subtotal = cot.Subtotal,
                    DescuentoTotal = cot.DescuentoTotal,
                    Impuestos = cot.Impuestos,
                    Total = cot.Total,
                    Estado = cot.Estado,
                    Moneda = cot.Moneda,
                    Origen = cot.Origen,
                    UsuarioCreador = cot.UsuarioCreador,
                    UsuarioActualiza = cot.UsuarioActualiza,
                    UsuarioAprueba = cot.UsuarioAprueba,
                    Notas = cot.Notas,
                    aplicacion = cot.aplicacion,
                    Detalles = detalles
                        .Where(d => d.IdCotizacion == cot.IdCotizacion)
                        .Select(d => new QuoteDetailResponse
                        {
                            IdDetalleCotizacion = d.IdDetalleCotizacion,
                            IdCotizacion = d.IdCotizacion,
                            IdArticulo = d.IdArticulo,
                            NombreArticulo = d.NombreArticulo,
                            PrecioUnitario = d.PrecioUnitario,
                            Cantidad = d.Cantidad,
                            DescuentoAplicado = d.DescuentoAplicado,
                            Subtotal = d.Subtotal,
                            Impuestos = d.Impuestos,
                            Total = d.Total,
                            FechaCreacion = d.FechaCreacion,
                            FechaActualizacion = d.FechaActualizacion,
                            UsuarioCreador = d.UsuarioCreador,
                            UsuarioActualiza = d.UsuarioActualiza,
                            aplicacion = d.aplicacion
                        }).ToList()
                }).ToList();
            }
        }







    }
}
