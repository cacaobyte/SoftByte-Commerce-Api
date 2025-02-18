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
        /// <param name="userName">Usuario que crea la bodega</param>
        /// <returns>Mensaje de éxito o error</returns>
        public string AddWarehouse(CreateWarehouse newWarehouse, string userName, int aplication)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Obtener el último ID de bodega con el formato "BXXX"
                    var lastWarehouse = db.Bodegas
                        .Where(b => b.Bodega1.StartsWith("B"))
                        .OrderByDescending(b => b.Bodega1)
                        .FirstOrDefault();

                    bool isCentralWarehouse = newWarehouse.Bodegacentral == true;
                    bool isSecundaryWarehouse = !isCentralWarehouse;

                    // Obtener el siguiente número en la secuencia
                    int nextNumber = 1; // Si no hay registros, empieza en 1

                    if (lastWarehouse != null)
                    {
                        // Extraer el número después de "B"
                        string lastNumberStr = lastWarehouse.Bodega1.Substring(1); // Se toma todo después de "B"

                        if (int.TryParse(lastNumberStr, out int lastNumber))
                        {
                            nextNumber = lastNumber + 1;
                        }
                    }

                    // Generar el nuevo ID con el formato "BXXX"
                    string newBodegaId = $"B{nextNumber:D3}"; // Mantiene tres dígitos sin el doble cero

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
                        Correo = newWarehouse.Correo,
                        Aplicacion = aplication
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
        /// Actualiza los datos de una bodega existente.
        /// </summary>
        /// <param name="bodegaId">ID de la bodega a actualizar</param>
        /// <param name="updatedWarehouse">Objeto con los datos actualizados de la bodega</param>
        /// <param name="userName">Usuario que realiza la actualización</param>
        /// <returns>Mensaje de éxito o error</returns>
        public string UpdateWarehouse(string bodegaId, CreateWarehouse updatedWarehouse, string userName)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // Buscar la bodega en la base de datos
                    var existingWarehouse = db.Bodegas.FirstOrDefault(b => b.Bodega1 == bodegaId);

                    if (existingWarehouse == null)
                    {
                        return $"Error: La bodega con ID {bodegaId} no existe.";
                    }

                    // Actualizar los valores de la bodega con los nuevos datos, si se proporcionan
                    existingWarehouse.Descripcion = updatedWarehouse.Descripcion ?? existingWarehouse.Descripcion;
                    existingWarehouse.Activo = updatedWarehouse.Activo ?? existingWarehouse.Activo;
                    existingWarehouse.Fechaactualizacion = DateTime.Now;
                    existingWarehouse.Updatedby = userName; // Registrar quién hizo la actualización
                    existingWarehouse.Departamento = updatedWarehouse.Departamento ?? existingWarehouse.Departamento;
                    existingWarehouse.Municipio = updatedWarehouse.Municipio ?? existingWarehouse.Municipio;
                    existingWarehouse.Direccion = updatedWarehouse.Direccion ?? existingWarehouse.Direccion;
                    existingWarehouse.Telefono = updatedWarehouse.Telefono ?? existingWarehouse.Telefono;
                    existingWarehouse.Bodegacentral = updatedWarehouse.Bodegacentral ?? existingWarehouse.Bodegacentral;
                    existingWarehouse.Bodegasecundaria = !updatedWarehouse.Bodegacentral ?? existingWarehouse.Bodegasecundaria;
                    existingWarehouse.Region = updatedWarehouse.Region ?? existingWarehouse.Region;
                    existingWarehouse.Correo = updatedWarehouse.Correo ?? existingWarehouse.Correo;

                    // Guardar cambios en la base de datos
                    db.SaveChanges();

                    return $"Bodega {bodegaId} actualizada con éxito.";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la bodega: " + ex.Message);
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
