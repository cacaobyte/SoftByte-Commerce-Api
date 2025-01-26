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
        public List<Articulo> GetArticles()
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Filtra artículos que no son de mayoreo (no empiezan con 'M')
                    return db.Articulos
                             .Where(a => !string.IsNullOrEmpty(a.Articulo1) && !a.Articulo1.StartsWith("M"))
                             .ToList();
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
        public Articulo CreateArticle(Articulo newArticle, IFormFile imageFile, string userName)
        {
            // Carpeta específica para artículos en Cloudinary
            string folder = "SoftByte/Commerce/Articulos";

            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Verificar que se haya enviado un archivo de imagen
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        // Subir la imagen a Cloudinary
                        string imageUrl = blUploadImages.UploadImage(imageFile, folder);

                        // Asignar la URL de la imagen al artículo
                        newArticle.Foto = imageUrl;
                    }

                    // Asignar campos de auditoría
                    newArticle.Createdby = userName;
                    newArticle.Fechacreacion = DateTime.Now;

                    // Agregar el artículo a la base de datos
                    db.Articulos.Add(newArticle);
                    db.SaveChanges();

                    return newArticle;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating the article.", ex);
            }
        }




    }
}
