using System.Collections.Generic;
using System.Linq;
using CC;
using CommerceCore.DAL.Commerce;
using CommerceCore.DAL.Commerce.Models;
using CommerceCore.DAL.Commerce.Models.SoftByteCommerce;
using CommerceCore.BL.cc.cloudinary;
using Microsoft.AspNetCore.Http;

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

        public List<Articulo> GetArticles()
        {
            List<Articulo> articles = new List<Articulo>();
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Retrieve articles from the database and assign them to the variable
                    articles = db.Articulos.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the articles.", ex);
            }
            return articles;
        }


        /// <summary>
        /// Obtiene todas las existencias de bodegas
        /// </summary>
        /// <returns>Lista de ExistenciaBodega</returns>
        public List<ExistenciaBodega> GetAllWarehouseStocks()
        {
            List<ExistenciaBodega> warehouseStocks = new List<ExistenciaBodega>();
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Obtiene todas las existencias de bodegas
                    warehouseStocks = db.ExistenciaBodegas.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving warehouse stocks.", ex);
            }

            return warehouseStocks;
        }

        /// <summary>
        /// Obtiene todas las existencias de bodegas
        /// </summary>
        /// <returns>Lista de ExistenciaBodega</returns>
        public List<Bodega> GetAllWarehouse()
        {
            List<Bodega> warehouseAll = new List<Bodega>();
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Obtiene todas las existencias de bodegas
                    warehouseAll = db.Bodegas.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving warehouse stocks.", ex);
            }

            return warehouseAll;
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
