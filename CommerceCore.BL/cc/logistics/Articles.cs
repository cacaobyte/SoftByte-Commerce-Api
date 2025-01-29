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
        public List<ArticleView> GetArticles()
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Filtra artículos que no son de mayoreo (no empiezan con 'M') y los proyecta a ArticleView
                    return db.Articulos
                             .Where(a => !string.IsNullOrEmpty(a.Articulo1) && !a.Articulo1.StartsWith("M"))
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
        /// Obtiene todas las existencias de artículos que son para mayoreo
        /// </summary>
        /// <returns>Lista de artículos para mayoreo</returns>
        public List<Articulo> GetWholesaleItems()
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Filtra artículos que son de mayoreo (empiezan con 'M')
                    return db.Articulos
                             .Where(a => !string.IsNullOrEmpty(a.Articulo1) && a.Articulo1.StartsWith("M"))
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
        public List<ExistenciaBodega> GetAllWarehouseStocks()
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
        /// Obtiene todas las existencias de bodegas
        /// </summary>
        /// <returns>Lista de Bodega (ML)</returns>
        public List<Bodega> GetAllWarehouse()
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Mapear manualmente el modelo DAL al modelo ML
                    return db.Bodegas.Select(b => new Bodega
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
                        ExistenciaBodegas = b.ExistenciaBodegas.Select(eb => new ExistenciaBodega
                        {
                            Articulo = eb.Articulo,
                            Bodega = eb.Bodega,
                            CantDisponible = eb.CantDisponible
                        }).ToList()
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving warehouses.", ex);
            }
        }





        /// <summary>
        /// Crea un nuevo artículo con su información y sube la imagen a Cloudinary.
        /// </summary>
        /// <param name="newArticle">El objeto Articulo que contiene la información del artículo.</param>
        /// <param name="imageFile">El archivo de imagen enviado desde el frontend.</param>
        /// <param name="userName">El nombre del usuario que crea el artículo.</param>
        /// <returns>El artículo creado con su URL de imagen.</returns>
        public Articulo CreateArticle(CreateArticle newArticleData, IFormFile imageFile, string userName)
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
                        // Extraer el número del último código de artículo y sumarle 1
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
                        Foto = null, // Se asignará después si hay imagen
                        Categoria = newArticleData.Categoria,
                        Precio = newArticleData.Precio,
                        PesoNeto = newArticleData.PesoNeto,
                        PesoBruto = newArticleData.PesoBruto,
                        Volumen = newArticleData.Volumen,
                        Activo = newArticleData.Activo,
                        Createdby = userName,
                        Fechacreacion = DateTime.Now,
                        SubCategoria = newArticleData.SubCategoria,
                        Clasificacion = newArticleData.Clasificacion
                    };

                    // 🔹 Subir imagen a Cloudinary si existe
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        string imageUrl = blUploadImages.UploadImage(imageFile, folder);
                        newArticulo.Foto = imageUrl;
                    }

                    // 🔹 Guardar en la base de datos
                    db.Articulos.Add(newArticulo);
                    db.SaveChanges();

                    return newArticulo;
                }
            }
            catch (Exception ex)
            {
                throw new Exception( ex.Message );
            }
        }








    }
}
