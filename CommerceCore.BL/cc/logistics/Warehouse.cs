using System.Collections.Generic;
using System.Linq;
using CC;
using CommerceCore.DAL.Commerce;
using CommerceCore.ML;
using CommerceCore.ML.cc.sale;
using CommerceCore.ML.cc.sale.Warehouse;

namespace CommerceCore.BL.cc.logistics
{
    public class Warehouse : LogicBase
    {

        public Warehouse(Configuration settings) {
            configuration = settings;
        }


        /// <summary>
        /// Agrega una nueva bodega al sistema generando automáticamente el ID.
        /// </summary>
        /// <param name="newWarehouse">Objeto con los datos de la bodega</param>
        /// <returns>Mensaje de éxito o error</returns>
        public string AddWarehouse(CreateWarehouse newWarehouse, string userName)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Obtener el último ID de bodega con el formato "B00X"
                    var lastWarehouse = db.Bodegas
                        .Where(b => b.Bodega1.StartsWith("B00"))
                        .OrderByDescending(b => b.Bodega1)
                        .FirstOrDefault();

                    bool isCentralWarehouse; 
                    bool isSecundaryWarehouse;


                    if (newWarehouse.Bodegacentral == true)
                    {
                        isCentralWarehouse = true;
                        isSecundaryWarehouse = false;
                    }
                    else
                    {
                        isCentralWarehouse = false;
                        isSecundaryWarehouse = true;
                    }

                    // Obtener el siguiente número en la secuencia
                    int nextNumber = 1; // Si no hay registros, empieza en 1

                    if (lastWarehouse != null && int.TryParse(lastWarehouse.Bodega1.Substring(3), out int lastNumber))
                    {
                        nextNumber = lastNumber + 1;
                    }

                    // Generar el nuevo ID siguiendo el formato "B00X"
                    string newBodegaId = $"B00{nextNumber:D1}";

                    // Validar que no haya una bodega con el mismo ID (caso improbable)
                    if (db.Bodegas.Any(b => b.Bodega1 == newBodegaId))
                    {
                        return $"Error: La bodega con ID {newBodegaId} ya existe. Intente de nuevo.";
                    }

                    // Crear la nueva entidad `Bodega` con los datos de `CreateWarehouse`
                    var bodega = new Bodega
                    {
                        Bodega1 = newBodegaId,
                        Descripcion = newWarehouse.Descripcion,
                        Activo = newWarehouse.Activo ?? true,
                        Createdby = userName,
                        Fechacreacion = DateTime.Now,
                        Fechaactualizacion = DateTime.Now,
                        Departamento = newWarehouse.Departamento,
                        Municipio = newWarehouse.Municipio,
                        Direccion = newWarehouse.Direccion,
                        Telefono = newWarehouse.Telefono,
                        Bodegacentral = isCentralWarehouse,
                        Bodegasecundaria = isSecundaryWarehouse,
                        Region = newWarehouse.Region,
                        Correo = newWarehouse.Correo
                    };

                    // Agregar la nueva bodega a la base de datos
                    db.Bodegas.Add(bodega);
                    db.SaveChanges();

                    return $"Bodega {bodega.Bodega1} creada con éxito.";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la bodega: " + ex.Message);
            }
        }




        /// <summary>
        /// Activa o desactiva una bodega estableciendo el campo Activo.
        /// </summary>
        /// <param name="warehouseId">El ID de la bodega</param>
        /// <returns>Mensaje indicando si la bodega fue activada o desactivada, o null si no se encontró</returns>
        public string? UpdateStatusWarehouse(string warehouseId)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Buscar la bodega por ID
                    var warehouse = db.Bodegas.FirstOrDefault(b => b.Bodega1 == warehouseId);

                    if (warehouse == null)
                    {
                        return null; // Devuelve null si no encuentra la bodega
                    }

                    // Asegurar que warehouse.Activo nunca sea null antes de cambiar su estado
                    bool estadoActual = warehouse.Activo.GetValueOrDefault(); // Si es null, se asigna false por defecto
                    warehouse.Activo = !estadoActual;
                    warehouse.Fechaactualizacion = DateTime.Now;

                    db.SaveChanges();

                    return warehouse.Activo.Value ? $"Bodega {warehouse.Bodega1} activada con éxito" : $"Bodega {warehouse.Bodega1} desactivada con éxito";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el estado de la bodega: " + ex.Message);
            }
        }

    }
}
