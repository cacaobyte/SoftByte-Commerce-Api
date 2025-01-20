using System.Collections.Generic;
using System.Linq;
using CC;
using CommerceCore.DAL.Commerce;
using CommerceCore.DAL.Commerce.Models;
using CommerceCore.DAL.Commerce.Models.SoftByteCommerce;

namespace CommerceCore.BL.cc.logistics
{
    public class Articles : LogicBase
    {
        public Articles(Configuration settings)
        {
            configuration = settings;
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


    }
}
