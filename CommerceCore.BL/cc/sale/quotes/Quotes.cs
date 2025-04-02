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


        /// <summary>
        /// Consulta los datos de la cotizaciones por mi tienda.
        /// </summary>
        /// <returns>Mensaje de éxito o error</returns>
        public List<QuoteResponse> GetQuotesStores(int aplication, string store)
        {
            using (var db = new SoftByte(configuration.appSettings.cadenaSql))
            {
                var result = db.Cotizaciones
                    .Where(c => c.aplicacion == aplication && c.Origen == store)
                    .Select(c => new QuoteResponse
                    {
                        IdCotizacion = c.IdCotizacion,
                        FechaCreacion = c.FechaCreacion,
                        FechaActualizacion = c.FechaActualizacion,
                        ClienteId = c.ClienteId,
                        NombreCliente = c.NombreCliente,
                        ApellidoCliente = c.ApellidoCliente,
                        Correo = c.Correo,
                        Telefono = c.Telefono,
                        TipoPago = c.TipoPago,
                        DescuentoCliente = c.DescuentoCliente,
                        Subtotal = c.Subtotal,
                        DescuentoTotal = c.DescuentoTotal,
                        Impuestos = c.Impuestos,
                        Total = c.Total,
                        Estado = c.Estado,
                        Moneda = c.Moneda,
                        Origen = c.Origen,
                        UsuarioCreador = c.UsuarioCreador,
                        UsuarioActualiza = c.UsuarioActualiza,
                        UsuarioAprueba = c.UsuarioAprueba,
                        Notas = c.Notas,
                        aplicacion = c.aplicacion,
                        Detalles = db.CotizacionDetalles
                            .Where(d => d.IdCotizacion == c.IdCotizacion && d.aplicacion == aplication)
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
                    })
                    .ToList();

                return result;
            }
        }

        /// Consulta los datos de mis cotizaciones.
        /// </summary>
        /// <returns>Mensaje de éxito o error</returns>
        public List<QuoteResponse> GetMyQuotes(int aplication, string userName)
        {
            using (var db = new SoftByte(configuration.appSettings.cadenaSql))
            {
                var result = db.Cotizaciones
                    .Where(c => c.aplicacion == aplication && c.UsuarioCreador == userName)
                    .Select(c => new QuoteResponse
                    {
                        IdCotizacion = c.IdCotizacion,
                        FechaCreacion = c.FechaCreacion,
                        FechaActualizacion = c.FechaActualizacion,
                        ClienteId = c.ClienteId,
                        NombreCliente = c.NombreCliente,
                        ApellidoCliente = c.ApellidoCliente,
                        Correo = c.Correo,
                        Telefono = c.Telefono,
                        TipoPago = c.TipoPago,
                        DescuentoCliente = c.DescuentoCliente,
                        Subtotal = c.Subtotal,
                        DescuentoTotal = c.DescuentoTotal,
                        Impuestos = c.Impuestos,
                        Total = c.Total,
                        Estado = c.Estado,
                        Moneda = c.Moneda,
                        Origen = c.Origen,
                        UsuarioCreador = c.UsuarioCreador,
                        UsuarioActualiza = c.UsuarioActualiza,
                        UsuarioAprueba = c.UsuarioAprueba,
                        Notas = c.Notas,
                        aplicacion = c.aplicacion,
                        Detalles = db.CotizacionDetalles
                            .Where(d => d.IdCotizacion == c.IdCotizacion && d.aplicacion == aplication)
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
                    })
                    .ToList();

                return result;
            }
        }


        public Dictionary<string, object> GetQuotesDashboard(int aplication)
        {
            using (var db = new SoftByte(configuration.appSettings.cadenaSql))
            {
                var cotizaciones = db.Cotizaciones
                    .Where(c => c.aplicacion == aplication)
                    .ToList();

                var detalles = db.CotizacionDetalles
                    .Where(d => d.aplicacion == aplication)
                    .ToList();

                var totalCotizaciones = cotizaciones.Count;
                var totalIngresos = cotizaciones.Sum(c => c.Total);
                var totalDescuentos = cotizaciones.Sum(c => c.DescuentoTotal);
                var totalImpuestos = cotizaciones.Sum(c => c.Impuestos);

                var cotizacionesPorEstado = cotizaciones
                    .GroupBy(c => c.Estado)
                    .Select(g => new { estado = g.Key, cantidad = g.Count() })
                    .ToList();

                var cotizacionesPorOrigen = cotizaciones
                    .GroupBy(c => c.Origen)
                    .Select(g => new { origen = g.Key, cantidad = g.Count() })
                    .ToList();

                var totalesPorMes = cotizaciones
                    .GroupBy(c => new { mes = c.FechaCreacion.Month, año = c.FechaCreacion.Year })
                    .Select(g => new {
                        mes = g.Key.mes,
                        año = g.Key.año,
                        total = g.Sum(c => c.Total)
                    })
                    .OrderBy(g => g.año).ThenBy(g => g.mes)
                    .ToList();

                var cotizacionesPorUsuario = cotizaciones
                    .GroupBy(c => c.UsuarioCreador)
                    .Select(g => new {
                        usuario = g.Key,
                        cantidad = g.Count(),
                        total = g.Sum(c => c.Total)
                    })
                    .OrderByDescending(g => g.cantidad)
                    .ToList();

                var articulosMasCotizados = detalles
                    .GroupBy(d => d.NombreArticulo)
                    .Select(g => new {
                        articulo = g.Key,
                        cantidadTotal = g.Sum(d => d.Cantidad),
                        vecesCotizado = g.Count()
                    })
                    .OrderByDescending(g => g.cantidadTotal)
                    .Take(5)
                    .ToList();

                var promedioArticulosPorCotizacion = totalCotizaciones > 0
                    ? detalles.Count / (double)totalCotizaciones
                    : 0;

                var promedioMensualCotizaciones = cotizaciones
                    .GroupBy(c => new { c.FechaCreacion.Year, c.FechaCreacion.Month })
                    .Select(g => g.Count())
                    .DefaultIfEmpty(0)
                    .Average();

                var tiposPago = cotizaciones
                    .GroupBy(c => c.TipoPago)
                    .Select(g => new { tipo = g.Key, cantidad = g.Count() })
                    .ToList();

                var clientesUnicos = cotizaciones
                    .Select(c => c.Correo?.ToLower().Trim())
                    .Where(correo => !string.IsNullOrEmpty(correo))
                    .Distinct()
                    .Count();

                return new Dictionary<string, object>
        {
            { "totalCotizaciones", totalCotizaciones },
            { "totalIngresos", totalIngresos },
            { "totalDescuentos", totalDescuentos },
            { "totalImpuestos", totalImpuestos },
            { "cotizacionesPorEstado", cotizacionesPorEstado },
            { "cotizacionesPorOrigen", cotizacionesPorOrigen },
            { "totalesPorMes", totalesPorMes },
            { "cotizacionesPorUsuario", cotizacionesPorUsuario },
            { "articulosMasCotizados", articulosMasCotizados },
            { "promedioArticulosPorCotizacion", promedioArticulosPorCotizacion },
            { "promedioMensualCotizaciones", promedioMensualCotizaciones },
            { "tiposPago", tiposPago },
            { "clientesUnicos", clientesUnicos }
        };
            }
        }





    }
}
