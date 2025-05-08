using System.Collections.Generic;
using System.Linq;
using CC;
using CommerceCore.DAL.Commerce;
using CommerceCore.ML;
using CommerceCore.DAL.Commerce.Models.SoftByteCommerce;
using CommerceCore.BL.cc.cloudinary;
using Microsoft.AspNetCore.Http;
using ExistenciaBodega = CommerceCore.ML.ExistenciaBodega;
using Bodega = CommerceCore.ML.Bodega;
using CommerceCore.ML.cc.sale;
using CommerceCore.ML.Commerce;

namespace CommerceCore.BL.cc.logistics
{
    public class Articles : LogicBase
    {
        private UploadImages blUploadImages;
        public Articles(Configuration settings)
        {
            configuration = settings;
            blUploadImages = new UploadImages(settings);
        }

        /// <summary>
        /// Obtiene todas las existencias de artículos que no son para mayoreo
        /// </summary>
        /// <returns>Lista de artículos para los clientes</returns>
        public List<ArticleView> GetArticles( int aplication)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Filtra artículos que no son de mayoreo (no empiezan con 'M') y los proyecta a ArticleView
                    return db.Articulos
                             .Where(a => !string.IsNullOrEmpty(a.Articulo1) && !a.Articulo1.StartsWith("M") && a.Activo == true && a.Aplicacion == aplication)
                             .Select(a => new ArticleView
                             {
                                 Articulo1 = a.Articulo1,
                                 Descripcion = a.Descripcion,
                                 Foto = a.Foto,
                                 Categoria = a.Categoria,
                                 Precio = a.Precio,
                                 PesoNeto = a.PesoNeto,
                                 PesoBruto = a.PesoBruto,
                                 Volumen = a.Volumen,
                                 Activo = a.Activo,
                                 Createdby = a.Createdby,
                                 Updatedby = a.Updatedby,
                                 Fechacreacion = a.Fechacreacion,
                                 Fechaactualizacion = a.Fechaactualizacion,
                                 SubCategoria = a.SubCategoria,
                                 clasificación = a.Clasificacion,
                             })
                             .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the articles.", ex);
            }
        }

        /// <summary>
        /// Obtiene todos los productos del commerce
        /// </summary>
        /// <returns>Lista de productos para los clientes del commerce</returns>
        public List<ArticuloAgrupadoDto> GetProductosCommerce(int idAplication)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    var articulosPlano = db.Articulos
                        .Join(db.ExistenciaBodegas, a => a.Articulo1, eb => eb.Articulo, (a, eb) => new { a, eb })
                        .Join(db.Bodegas, ab => ab.eb.Bodega, b => b.Bodega1, (ab, b) => new
                        {
                            Articulo = ab.a.Articulo1,
                            Descripcion = ab.a.Descripcion,
                            Foto = ab.a.Foto,
                            Categoria = ab.a.Categoria,
                            Clasificacion = ab.a.Clasificacion,
                            Precio = ab.a.Precio,
                            AplicacionArticulo = ab.a.Aplicacion,
                            AplicacionBodega = b.Aplicacion,
                            BodegaId = b.Bodega1,
                            Disponible = ab.eb.CantDisponible ?? 0,
                            Ubicacion = b.Direccion ?? string.Empty
                        })
                        .Where(x =>
                            !string.IsNullOrEmpty(x.Articulo) &&
                            !x.Articulo.StartsWith("M") &&
                            x.AplicacionArticulo == idAplication &&
                            x.AplicacionBodega == idAplication
                        )
                        .ToList();

                    // Agrupar por artículo
                    var resultado = articulosPlano
                        .GroupBy(x => x.Articulo)
                        .Select(grp =>
                        {
                            var primero = grp.First();

                            var dto = new ArticuloAgrupadoDto
                            {
                                Articulo = primero.Articulo,
                                Descripcion = primero.Descripcion,
                                Foto = primero.Foto,
                                Categoria = primero.Categoria,
                                Clasificacion = primero.Clasificacion,
                                VariantesPorBodega = new Dictionary<string, VarianteBodegaDto>()
                            };

                            foreach (var item in grp)
                            {
                                dto.VariantesPorBodega[item.BodegaId] = new VarianteBodegaDto
                                {
                                    Bodega = item.BodegaId,
                                    Precio = item.Precio,
                                    Disponible = item.Disponible,
                                    Ubicacion = item.Ubicacion
                                };
                            }

                            return dto;
                        })
                        .ToList();

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error al obtener los productos del commerce.", ex);
            }
        }




        /// <summary>
        /// Obtiene todas las existencias de artículos que no son para mayoreo
        /// </summary>
        /// <returns>Lista de artículos para los clientes</returns>
        public List<ArticleView> GetArticlesWarehouse(string warehouse)
        {
            try
            {
                if(warehouse == null)
                {
                    throw new Exception($"No se obtuvo la bodega para traer los articulos");
                }

                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    var result = db.Articulos
                                    .Join( db.ExistenciaBodegas,   a => a.Articulo1,  eb => eb.Articulo,  (a, eb) => new { a, eb } )
                                    .Where(x =>  !string.IsNullOrEmpty(x.a.Articulo1) &&  !x.a.Articulo1.StartsWith("M") &&  x.eb.Bodega == warehouse  )
                                    .Select(x => new ArticleView
                                    {
                                        Articulo1 = x.a.Articulo1,
                                        Descripcion = x.a.Descripcion,
                                        Foto = x.a.Foto,
                                        Categoria = x.a.Categoria,
                                        Precio = x.a.Precio,
                                        PesoNeto = x.a.PesoNeto,
                                        PesoBruto = x.a.PesoBruto,
                                        Volumen = x.a.Volumen,
                                        Activo = x.a.Activo,
                                        Createdby = x.a.Createdby,
                                        Updatedby = x.a.Updatedby,
                                        Fechacreacion = x.a.Fechacreacion,
                                        Fechaactualizacion = x.a.Fechaactualizacion,
                                        SubCategoria = x.a.SubCategoria,
                                        clasificación = x.a.Clasificacion,
                                        disponible = x.eb.CantDisponible,
                                        existencias = x.eb.CantDisponible + x.eb.CantReservada,
                                        reservada = x.eb.CantReservada,
                                        totalValorProducto = x.eb.CantDisponible * x.a.Precio,
                                        bodega = x.eb.Bodega,
                                    })
                                    .ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the articles.", ex);
            }
        }

        /// <summary>
        /// Obtiene todas las existencias de artículos que no son para mayoreo y bodega selecionada de cotizaciones
        /// </summary>
        /// <returns>Lista de artículos para los clientes</returns>
        public List<ArticleView> GetArticlesSelectedWarehouse(string warehouse)
        {
            try
            {
                if (warehouse == null)
                {
                    throw new Exception($"No se obtuvo la bodega para traer los articulos");
                }

                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    var result = db.Articulos
                                    .Join(db.ExistenciaBodegas, a => a.Articulo1, eb => eb.Articulo, (a, eb) => new { a, eb })
                                    .Where(x => !string.IsNullOrEmpty(x.a.Articulo1) && !x.a.Articulo1.StartsWith("M") && x.eb.Bodega == warehouse)
                                    .Select(x => new ArticleView
                                    {
                                        Articulo1 = x.a.Articulo1,
                                        Descripcion = x.a.Descripcion,
                                        Foto = x.a.Foto,
                                        Categoria = x.a.Categoria,
                                        Precio = x.a.Precio,
                                        PesoNeto = x.a.PesoNeto,
                                        PesoBruto = x.a.PesoBruto,
                                        Volumen = x.a.Volumen,
                                        Activo = x.a.Activo,
                                        Createdby = x.a.Createdby,
                                        Updatedby = x.a.Updatedby,
                                        Fechacreacion = x.a.Fechacreacion,
                                        Fechaactualizacion = x.a.Fechaactualizacion,
                                        SubCategoria = x.a.SubCategoria,
                                        clasificación = x.a.Clasificacion,
                                        disponible = x.eb.CantDisponible,
                                        existencias = x.eb.CantDisponible + x.eb.CantReservada,
                                        reservada = x.eb.CantReservada,
                                        totalValorProducto = x.eb.CantDisponible * x.a.Precio,
                                        bodega = x.eb.Bodega,
                                    })
                                    .ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the articles.", ex);
            }
        }

        /// <summary>
        /// Obtiene todas las existencias de artículos que son para mayoreo
        /// </summary>
        /// <returns>Lista de artículos para mayoreo</returns>
        public List<Articulo> GetWholesaleItems(int aplication)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Filtra artículos que son de mayoreo (empiezan con 'M')
                    return db.Articulos
                             .Where(a => !string.IsNullOrEmpty(a.Articulo1) && a.Articulo1.StartsWith("M") && a.Aplicacion == aplication)
                             .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving wholesale items.", ex);
            }
        }



        /// <summary>
        /// Obtiene todas las existencias de bodegas
        /// </summary>
        /// <returns>Lista de ExistenciaBodega (ML)</returns>
        public List<ExistenciaBodega> GetAllWarehouseStocks(int aplication)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Convierte el modelo DAL al modelo ML (con transformación)
                    return db.ExistenciaBodegas
                             .Select(e => new ExistenciaBodega
                             {
                                 Articulo = e.Articulo,
                                 Bodega = e.Bodega,
                                 ExistenciaMinima = e.ExistenciaMinima,
                                 ExistenciaMaxima = e.ExistenciaMaxima,
                                 PuntoDeReorden = e.PuntoDeReorden,
                                 CantDisponible = e.CantDisponible,
                                 CantReservada = e.CantReservada,
                                 CantNoAprobada = e.CantNoAprobada,
                                 CantVencida = e.CantVencida,
                                 CantTransito = e.CantTransito,
                                 CantProduccion = e.CantProduccion,
                                 CantPedida = e.CantPedida,
                                 CantRemitida = e.CantRemitida,
                                 Congelado = e.Congelado,
                                 FechaCong = e.FechaCong,
                                 BloqueaTrans = e.BloqueaTrans,
                                 FechaDescong = e.FechaDescong,
                                 Createdby = e.Createdby,
                                 Updatedby = e.Updatedby,
                                 Fechacreacion = e.Fechacreacion,
                                 Fechaactualizacion = e.Fechaactualizacion,
                                 CostoUntPromedioLoc = e.CostoUntPromedioLoc,
                                 CostoUntPromedioDol = e.CostoUntPromedioDol,
                                 CostoUntEstandarLoc = e.CostoUntEstandarLoc,
                                 CostoUntEstandarDol = e.CostoUntEstandarDol,
                                 CostoPromComparativoLoc = e.CostoPromComparativoLoc,
                                 CostoPromComparativoDolar = e.CostoPromComparativoDolar
                             }).Where(a => !a.Articulo.StartsWith("M"))
                             .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving warehouse stocks.", ex);
            }
        }



        /// <summary>
        /// Obtiene todas las  bodegas
        /// </summary>
        /// <returns>Lista de Bodega (ML)</returns>
        public List<Bodega> GetAllWarehouse(int aplication)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Mapear manualmente el modelo DAL al modelo ML
                    return db.Bodegas.Where(b => b.Aplicacion == aplication).Select(b => new Bodega
                    {
                        Bodega1 = b.Bodega1,
                        Descripcion = b.Descripcion,
                        Activo = b.Activo,
                        Createdby = b.Createdby,
                        Updatedby = b.Updatedby,
                        Fechacreacion = b.Fechacreacion,
                        Fechaactualizacion = b.Fechaactualizacion,
                        Departamento = b.Departamento,
                        Municipio = b.Municipio,
                        Direccion = b.Direccion,
                        Telefono = b.Telefono,
                        Bodegacentral = b.Bodegacentral,
                        Bodegasecundaria = b.Bodegasecundaria,
                        Region = b.Region,
                        Correo = b.Correo,
                        // Relación opcional: solo si necesitas cargar las existencias
                    /*    ExistenciaBodegas = b.ExistenciaBodegas.Select(eb => new ExistenciaBodega
                        {
                            Articulo = eb.Articulo,
                            Bodega = eb.Bodega,
                            CantDisponible = eb.CantDisponible
                        }).ToList()*/
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving warehouses.", ex);
            }
        }



        /// <summary>
        /// Obtiene todas las  bodegas
        /// </summary>
        /// <returns>Lista de Bodega (ML)</returns>
        public List<Bodega> GetAllWarehouseActive(int aplication)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Mapear manualmente el modelo DAL al modelo ML
                    return db.Bodegas.Where(b => b.Activo == true && b.Aplicacion == aplication).Select(b => new Bodega
                    {
                        Bodega1 = b.Bodega1,
                        Descripcion = b.Descripcion,
                        Activo = b.Activo,
                        Createdby = b.Createdby,
                        Updatedby = b.Updatedby,
                        Fechacreacion = b.Fechacreacion,
                        Fechaactualizacion = b.Fechaactualizacion,
                        Departamento = b.Departamento,
                        Municipio = b.Municipio,
                        Direccion = b.Direccion,
                        Telefono = b.Telefono,
                        Bodegacentral = b.Bodegacentral,
                        Bodegasecundaria = b.Bodegasecundaria,
                        Region = b.Region,
                        Correo = b.Correo,
                        // Relación opcional: solo si necesitas cargar las existencias
                        /*    ExistenciaBodegas = b.ExistenciaBodegas.Select(eb => new ExistenciaBodega
                            {
                                Articulo = eb.Articulo,
                                Bodega = eb.Bodega,
                                CantDisponible = eb.CantDisponible
                            }).ToList()*/
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving warehouses.", ex);
            }
        }


        /// <summary>
        /// Crea un nuevo artículo con su información y sube la imagen a Cloudflare.
        /// </summary>
        /// <param name="newArticleData">El objeto CreateArticle que contiene la información del artículo.</param>
        /// <param name="imageFile">El archivo de imagen enviado desde el frontend.</param>
        /// <param name="userName">El nombre del usuario que crea el artículo.</param>
        /// <returns>El artículo creado con su URL de imagen.</returns>
        public async Task<Articulo> CreateArticleAsync(CreateArticle newArticleData, IFormFile imageFile, string userName, int IdAplication)
        {
            string folder = "SoftByte/Commerce/Articulos";

            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // 🔹 Generar el nuevo código de artículo
                    var lastArticle = db.Articulos
                        .Where(a => a.Articulo1.StartsWith("A") && a.Articulo1.Length == 9)
                        .OrderByDescending(a => a.Articulo1)
                        .FirstOrDefault();

                    string newArticuloCode = "A00000001"; // Código inicial si no hay registros

                    if (lastArticle != null)
                    {
                        string lastCode = lastArticle.Articulo1.Substring(1);
                        if (int.TryParse(lastCode, out int lastNumber))
                        {
                            newArticuloCode = $"A{(lastNumber + 1):D8}";
                        }
                    }

                    // 🔹 Mapear los datos de `CreateArticle` a `Articulo`
                    var newArticulo = new Articulo
                    {
                        Articulo1 = newArticuloCode,
                        Descripcion = newArticleData.Descripcion,
                        Foto = null,
                        Categoria = newArticleData.Categoria,
                        Precio = newArticleData.Precio,
                        PesoNeto = newArticleData.PesoNeto,
                        PesoBruto = newArticleData.PesoBruto,
                        Volumen = newArticleData.Volumen,
                        Activo = newArticleData.Activo,
                        Createdby = userName,
                        Fechacreacion = DateTime.Now,
                        SubCategoria = newArticleData.SubCategoria,
                        Clasificacion = newArticleData.Clasificacion,
                        Aplicacion = IdAplication

                    };

                    // 🔹 Subir imagen a Cloudflare si existe
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        string imageUrl = await blUploadImages.UploadImageToCloudflare(imageFile, newArticuloCode);
                        newArticulo.Foto = imageUrl;
                    }

                    db.Articulos.Add(newArticulo);
                    await db.SaveChangesAsync();

                    return newArticulo;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en CreateArticle: {ex.Message}");
            }
        }









    }
}
