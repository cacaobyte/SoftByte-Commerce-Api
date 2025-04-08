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


namespace CommerceCore.BL.cc.Rrhh
{
    public class Departaments : LogicBase
    {
        public Departaments(Configuration settings)
        {
            configuration = settings;
        }


        /// <summary>
        /// Traer los departamentos de la compañia.
        /// </summary>
        /// <param name="request">Objeto con los datos del departamento de la compañia</param>
        /// <param name="userName">Usuario que crea el registro</param>
        /// <param name="aplication">Id de la aplicacion</param>
        /// <returns>Mensaje de éxito o error</returns>
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
        /// Agrega un nuevo departamnento.
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
                        throw new Exception("Los datos del departamento a crear se encuentran vacíos.");
                    }

                    DateTime fechaActual = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

                    // 🔹 Generar código único
                    string nombreSanitizado = new string(request.NombreDepartamento
                        .ToUpperInvariant()
                        .Where(c => char.IsLetterOrDigit(c))
                        .ToArray());

                    string guidSegment = Guid.NewGuid().ToString().Split('-')[0]; // parte corta
                    string codigoGenerado = $"{nombreSanitizado}_{guidSegment}";

                    // 🔍 Validar unicidad
                    bool codigoExiste = db.Departamentos.Any(d => d.CodigoDepartamento == codigoGenerado);
                    while (codigoExiste)
                    {
                        guidSegment = Guid.NewGuid().ToString().Split('-')[0];
                        codigoGenerado = $"{nombreSanitizado}_{guidSegment}";
                        codigoExiste = db.Departamentos.Any(d => d.CodigoDepartamento == codigoGenerado);
                    }

                    var newDepartament = new Departamento
                    {
                        NombreDepartamento = request.NombreDepartamento,
                        Descripcion = request.Descripcion,
                        CodigoDepartamento = codigoGenerado, // 🚀 generado internamente
                        UbicacionFisica = request.UbicacionFisica,
                        CorreoContacto = request.CorreoContacto,
                        ExtensionTelefonica = request.ExtensionTelefonica,
                        Estado = "Activo",
                        Observaciones = request.Observaciones,
                        CreatedBy = request.CreatedBy,
                        CreatedAt = fechaActual,
                        UpdatedBy = request.UpdatedBy,
                        UpdatedAt = fechaActual,
                        Aplicación = aplication,
                        telefono = request.telefono
                    };

                    db.Departamentos.Add(newDepartament);
                    db.SaveChanges();

                    if (newDepartament.IdDepartamento == Guid.Empty)
                    {
                        throw new Exception("No se pudo generar el ID del departamento.");
                    }

                    return newDepartament;
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
        public Departamento UpdateDepartamentsCompany(Departamento request, string userName, int aplicacion)
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
                    existingDepartamento.Aplicación = aplicacion;
                    existingDepartamento.UpdatedAt = DateTime.Now;
                    existingDepartamento.UpdatedBy = userName;
                    existingDepartamento.telefono = request.telefono;


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

