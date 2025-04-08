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
using CommerceCore.ML.cc.Rrhh;
using CommerceCore.ML.cc.sale.Quotes;

namespace CommerceCore.BL.cc.Rrhh
{
    public class Departaments : LogicBase
    {
        public Departaments(Configuration settings)
        {
            configuration = settings;
        }


        public List<Departamento> GetDepartamentsCompany(string userName, int aplicacion)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    var departamentList = new List<Departamento>();
                    departamentList = db.Departamentos.Where(d => d.Aplicación == aplicacion).ToList();
                    return departamentList;
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
        /// <param name="request">Objeto con los datos del departamento de la compañia</param>
        /// <param name="userName">Usuario que crea el registro</param>
        /// <param name="aplication">Id de la aplicacion</param>
        /// <returns>Mensaje de éxito o error</returns>
        public Departamento CreateDepartamentsCompany(DepartamentsRrhh request, string userName, int aplication)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    if (request == null)
                    {
                        throw new Exception("Los datos del departamento a crear se encuentran vacios");
                    }

                    // Convertir fechas para PostgreSQL
                    DateTime fechaActual = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

                    var newDepartaments = new Departamento
                    {
                        NombreDepartamento = request.NombreDepartamento,
                        Descripcion = request.Descripcion,
                        CodigoDepartamento = request.CodigoDepartamento,
                        UbicacionFisica = request.UbicacionFisica,
                        CorreoContacto = request.CorreoContacto,
                        ExtensionTelefonica = request.ExtensionTelefonica,
                        Estado = request.Estado,
                        Observaciones = request.Observaciones,
                        CreatedBy = request.CreatedBy,
                        CreatedAt = fechaActual,
                        UpdatedAt = fechaActual,
                        UpdatedBy = request.UpdatedBy,
                        Aplicación = aplication
                    };

                    db.Departamentos.Add(newDepartaments);
                    db.SaveChanges();

                    if (newDepartaments.IdDepartamento == Guid.Empty)
                    {
                        throw new Exception("No se pudo generar el ID del departamento.");
                    }


                    return newDepartaments;
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
        /// Actualiza un departamento existente.
        /// </summary>
        /// <param name="request">Objeto Departamento con los datos actualizados</param>
        /// <param name="userName">Usuario que realiza la actualización</param>
        /// <returns>Departamento actualizado</returns>
        public Departamento UpdateDepartamentsCompany(Departamento request, string userName)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    if (request == null || request.IdDepartamento == Guid.Empty)
                    {
                        throw new Exception("El objeto del departamento a actualizar no es válido.");
                    }

                    var existingDepartamento = db.Departamentos.FirstOrDefault(d => d.IdDepartamento == request.IdDepartamento);

                    if (existingDepartamento == null)
                    {
                        throw new Exception("No se encontró el departamento a actualizar.");
                    }

                    // Actualización de campos
                    existingDepartamento.NombreDepartamento = request.NombreDepartamento;
                    existingDepartamento.Descripcion = request.Descripcion;
                    existingDepartamento.CodigoDepartamento = request.CodigoDepartamento;
                    existingDepartamento.UbicacionFisica = request.UbicacionFisica;
                    existingDepartamento.CorreoContacto = request.CorreoContacto;
                    existingDepartamento.ExtensionTelefonica = request.ExtensionTelefonica;
                    existingDepartamento.Estado = request.Estado;
                    existingDepartamento.Observaciones = request.Observaciones;
                    existingDepartamento.Aplicación = request.Aplicación;
                    existingDepartamento.UpdatedAt = DateTime.Now;
                    existingDepartamento.UpdatedBy = userName;


                    db.SaveChanges();

                    return existingDepartamento;
                }
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Error al actualizar en la base de datos: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado: {ex.Message}");
            }
        }






    }
}

