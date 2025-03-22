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

        public List<Cotizacione> GetQuotes(string userName, int aplication)
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
                        Notas = request.Notas
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
                        UsuarioCreador = userName
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





    }
}
