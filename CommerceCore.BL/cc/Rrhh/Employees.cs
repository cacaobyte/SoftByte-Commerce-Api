using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommerceCore.ML;
using CommerceCore.DAL.Commerce;
using CC;
using CommerceCore.ML.cc.Rrhh;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;


namespace CommerceCore.BL.cc.Rrhh
{
    public class Employees : LogicBase
    {
        public Employees(Configuration settings) {
            configuration = settings;
        }


        /// <summary>
        /// Traer los empleados de la compañia.
        /// </summary>
        /// <param name="userName">Usuario que crea el registro</param>
        /// <param name="aplicacion">Id de la aplicacion</param>
        /// <returns>Mensaje de éxito o error</returns>
        public List<Empleado> GetEmployeesCompany(string userName, int aplicacion)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    var PositionsList = new List<Empleado>();
                    PositionsList = db.Empleados.Where(d => d.aplicacion == aplicacion).ToList();
                    return PositionsList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Agrega un nuevo empleado a la compañía.
        /// </summary>
        /// <param name="request">Datos del nuevo empleado</param>
        /// <param name="userName">Usuario que crea el empleado</param>
        /// <param name="idPuesto">ID del puesto asignado</param>
        /// <param name="idDepartamento">ID del departamento asignado</param>
        /// <param name="aplicacion">ID de la aplicación</param>
        /// <returns>Instancia del empleado creado</returns>
        public Empleado CreateEmployees(EmployeesRrhh request, string userName, Guid idPuesto,  int aplicacion)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    if (request == null)
                        throw new Exception("Los datos del empleado están vacíos.");

                    if (idPuesto == Guid.Empty )
                        throw new Exception("El ID del puesto o del departamento no es válido.");

                    DateTime fechaActual = DateTime.Now;

                    var newEmpleado = new Empleado
                    {
                        IdEmpleado = Guid.NewGuid(),
                        Nombres = request.Nombres,
                        Apellidos = request.Apellidos,
                        Genero = request.Genero,
                        FechaNacimiento = request.FechaNacimiento,
                        TipoDocumento = request.TipoDocumento,
                        NumeroDocumento = request.NumeroDocumento,
                        Nacionalidad = request.Nacionalidad,
                        Correo = request.Correo,
                        Telefono1 = request.Telefono1,
                        Telefono2 = request.Telefono2,
                        Direccion = request.Direccion,
                        DepartamentoResidencia = request.DepartamentoResidencia,
                        MunicipioResidencia = request.MunicipioResidencia,
                        Foto = request.Foto,
                        FechaIngreso = request.FechaIngreso,
                        FechaEgreso = request.FechaEgreso,
                        TipoContrato = request.TipoContrato,
                        TipoEmpleado = request.TipoEmpleado,
                        Jornada = request.Jornada,
                        Salario = request.Salario,
                        EstadoEmpleado = request.EstadoEmpleado ?? "Activo",
                        TipoSangre = request.TipoSangre,
                        ContactoEmergenciaNombre = request.ContactoEmergenciaNombre,
                        ContactoEmergenciaParentesco = request.ContactoEmergenciaParentesco,
                        ContactoEmergenciaTelefono = request.ContactoEmergenciaTelefono,
                        ContactoEmergenciaDireccion = request.ContactoEmergenciaDireccion,
                        Observaciones = request.Observaciones,
                        NotasInternas = request.NotasInternas,
                        CreatedBy = userName,
                        CreatedAt = fechaActual,
                        UpdatedBy = userName,
                        UpdatedAt = fechaActual,
                        Puesto = idPuesto,
                        Departamento = request.Departamento,
                        aplicacion = aplicacion
                    };

                    db.Empleados.Add(newEmpleado);
                    db.SaveChanges();

                    return newEmpleado;
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
        /// Actualiza los datos de un empleado existente.
        /// </summary>
        /// <param name="request">Datos actualizados del empleado</param>
        /// <param name="userName">Usuario que actualiza el registro</param>
        /// <param name="aplicacion">ID de la aplicación</param>
        /// <returns>Instancia del empleado actualizado</returns>
        public Empleado UpdateEmployee(Empleado request, string userName, int aplicacion)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    if (request == null || request.IdEmpleado == Guid.Empty)
                        throw new Exception("Los datos del empleado a actualizar no son válidos.");

                    var empleadoExistente = db.Empleados.FirstOrDefault(e =>
                        e.IdEmpleado == request.IdEmpleado && e.aplicacion == aplicacion);

                    if (empleadoExistente == null)
                        throw new Exception("El empleado que desea actualizar no existe.");

                    // 🔁 Actualización de campos
                    empleadoExistente.Nombres = request.Nombres;
                    empleadoExistente.Apellidos = request.Apellidos;
                    empleadoExistente.Genero = request.Genero;
                    empleadoExistente.FechaNacimiento = request.FechaNacimiento;
                    empleadoExistente.TipoDocumento = request.TipoDocumento;
                    empleadoExistente.NumeroDocumento = request.NumeroDocumento;
                    empleadoExistente.Nacionalidad = request.Nacionalidad;
                    empleadoExistente.Correo = request.Correo;
                    empleadoExistente.Telefono1 = request.Telefono1;
                    empleadoExistente.Telefono2 = request.Telefono2;
                    empleadoExistente.Direccion = request.Direccion;
                    empleadoExistente.DepartamentoResidencia = request.DepartamentoResidencia;
                    empleadoExistente.MunicipioResidencia = request.MunicipioResidencia;
                    empleadoExistente.Foto = request.Foto;
                    empleadoExistente.FechaIngreso = request.FechaIngreso;
                    empleadoExistente.FechaEgreso = request.FechaEgreso;
                    empleadoExistente.TipoContrato = request.TipoContrato;
                    empleadoExistente.TipoEmpleado = request.TipoEmpleado;
                    empleadoExistente.Jornada = request.Jornada;
                    empleadoExistente.Salario = request.Salario;
                    empleadoExistente.EstadoEmpleado = request.EstadoEmpleado ?? empleadoExistente.EstadoEmpleado;
                    empleadoExistente.TipoSangre = request.TipoSangre;
                    empleadoExistente.ContactoEmergenciaNombre = request.ContactoEmergenciaNombre;
                    empleadoExistente.ContactoEmergenciaParentesco = request.ContactoEmergenciaParentesco;
                    empleadoExistente.ContactoEmergenciaTelefono = request.ContactoEmergenciaTelefono;
                    empleadoExistente.ContactoEmergenciaDireccion = request.ContactoEmergenciaDireccion;
                    empleadoExistente.Observaciones = request.Observaciones;
                    empleadoExistente.NotasInternas = request.NotasInternas;
                    empleadoExistente.Puesto = request.Puesto;
                    empleadoExistente.Departamento = request.Departamento;
                    empleadoExistente.UpdatedBy = userName;
                    empleadoExistente.UpdatedAt = DateTime.Now;
                    empleadoExistente.aplicacion = aplicacion;

                    db.SaveChanges();

                    return empleadoExistente;
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
