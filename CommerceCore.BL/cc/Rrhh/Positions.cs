﻿using System.Text;
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
    public class Positions : LogicBase
    {
        public Positions(Configuration settings)
        {
            configuration = settings;
        }

        /// <summary>
        /// Traer los puestos de la compañia.
        /// </summary>
        /// <param name="userName">Usuario que crea el registro</param>
        /// <param name="aplicacion">Id de la aplicacion</param>
        /// <returns>Mensaje de éxito o error</returns>
        public List<Puesto> GetPositionsCompany(string userName, int aplicacion)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    var PositionsList = new List<Puesto>();
                    PositionsList = db.Puestos.Where(d => d.Aplicación == aplicacion ).ToList();
                    return PositionsList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        /// <summary>
        /// Agrega un nuevo puesto a un departamento.
        /// </summary>
        /// <param name="request">Datos del nuevo puesto</param>
        /// <param name="userName">Usuario que crea el puesto</param>
        /// <param name="idDepartamento">ID del departamento al que pertenece</param>
        /// <param name="aplicacion">ID de la aplicación</param>
        /// <returns>Instancia del puesto creado</returns>
        public Puesto CreatePosition(PositionsRrhh request, string userName, Guid idDepartamento, int aplicacion)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    if (request == null)
                        throw new Exception("Los datos del puesto están vacíos.");

                    DateTime fechaActual = DateTime.Now;

                    // 🔹 Generar código único
                    string nombreSanitizado = new string(request.NombrePuesto
                        .ToUpperInvariant()
                        .Where(c => char.IsLetterOrDigit(c))
                        .ToArray());

                    string guidSegment = Guid.NewGuid().ToString().Split('-')[0];
                    string codigoGenerado = $"{nombreSanitizado}_{guidSegment}";

                    // Validar unicidad del código
                    while (db.Puestos.Any(p => p.CodigoPuesto == codigoGenerado))
                    {
                        guidSegment = Guid.NewGuid().ToString().Split('-')[0];
                        codigoGenerado = $"{nombreSanitizado}_{guidSegment}";
                    }

                    var newPuesto = new Puesto
                    {
                        IdPuesto = Guid.NewGuid(),
                        IdDepartamento = idDepartamento,
                        NombreDepartamento = request.NombreDepartamento,
                        NombrePuesto = request.NombrePuesto,
                        Descripcion = request.Descripcion,
                        CodigoPuesto = codigoGenerado,
                        NivelJerarquico = request.NivelJerarquico,
                        TipoPuesto = request.TipoPuesto,
                        SueldoBase = request.SueldoBase,
                        ModalidadTrabajo = request.ModalidadTrabajo,
                        RequiereSupervision = request.RequiereSupervision,
                        EsPuestoBase = request.EsPuestoBase,
                        CantidadVacantes = request.CantidadVacantes,
                        HorarioTrabajo = request.HorarioTrabajo,
                        RequisitosMinimos = request.RequisitosMinimos,
                        CapacitacionRequerida = request.CapacitacionRequerida,
                        RiesgoLaboral = request.RiesgoLaboral,
                        HerramientasUtilizadas = request.HerramientasUtilizadas,
                        UniformeRequerido = request.UniformeRequerido,
                        Observaciones = request.Observaciones,
                        Estado = "Activo",
                        CreatedBy = userName,
                        CreatedAt = fechaActual,
                        UpdatedBy = userName,
                        UpdatedAt = fechaActual,
                        Aplicación = aplicacion
                    };

                    db.Puestos.Add(newPuesto);
                    db.SaveChanges();

                    return newPuesto;
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
        /// Actualiza los datos de un puesto existente.
        /// </summary>
        /// <param name="request">Datos actualizados del puesto</param>
        /// <param name="userName">Usuario que actualiza el puesto</param>
        /// <param name="aplicacion">ID de la aplicación</param>
        /// <returns>Instancia del puesto actualizado</returns>
        public Puesto UpdatePosition(Puesto request, string userName, int aplicacion)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    if (request == null || request.IdPuesto == Guid.Empty)
                        throw new Exception("Los datos del puesto a actualizar no son válidos.");

                    var puestoExistente = db.Puestos.FirstOrDefault(p =>
                        p.IdPuesto == request.IdPuesto && p.Aplicación == aplicacion);

                    if (puestoExistente == null)
                        throw new Exception("El puesto que desea actualizar no existe.");

                    // Actualizar campos
                    puestoExistente.NombrePuesto = request.NombrePuesto;
                    puestoExistente.NombreDepartamento = request.NombreDepartamento;
                    puestoExistente.Descripcion = request.Descripcion;
                    puestoExistente.NivelJerarquico = request.NivelJerarquico;
                    puestoExistente.TipoPuesto = request.TipoPuesto;
                    puestoExistente.SueldoBase = request.SueldoBase;
                    puestoExistente.ModalidadTrabajo = request.ModalidadTrabajo;
                    puestoExistente.RequiereSupervision = request.RequiereSupervision;
                    puestoExistente.EsPuestoBase = request.EsPuestoBase;
                    puestoExistente.CantidadVacantes = request.CantidadVacantes;
                    puestoExistente.HorarioTrabajo = request.HorarioTrabajo;
                    puestoExistente.RequisitosMinimos = request.RequisitosMinimos;
                    puestoExistente.CapacitacionRequerida = request.CapacitacionRequerida;
                    puestoExistente.RiesgoLaboral = request.RiesgoLaboral;
                    puestoExistente.HerramientasUtilizadas = request.HerramientasUtilizadas;
                    puestoExistente.UniformeRequerido = request.UniformeRequerido;
                    puestoExistente.Observaciones = request.Observaciones;
                    puestoExistente.Estado = request.Estado ?? puestoExistente.Estado;
                    puestoExistente.UpdatedBy = userName;
                    puestoExistente.UpdatedAt = DateTime.Now;
                    puestoExistente.Aplicación = aplicacion;

                    db.SaveChanges();

                    return puestoExistente;
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
