using System;
using System.Collections.Generic;
using System.Linq;
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
using CommerceCore.ML.cc.sale.Clients;

namespace CommerceCore.BL.cc.sale.Clientes
{
    public class Clientes : LogicBase
    {
        private UploadImages blUploadImages;
        public Clientes(Configuration settings) {
            configuration = settings;
            blUploadImages = new UploadImages(settings);
        }

        public List<Cliente> GetClient(string userName, int aplication)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    var clients = new List<Cliente>();
                    clients = db.Clientes.Where(c => c.Activo == true && c.aplicacion == aplication).ToList();
                    return clients;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de información de contacto de todos los clientes de una aplicación específica.
        /// </summary>
        /// <param name="aplication">ID de la aplicación</param>
        /// <returns>Lista de objetos ContactClients con la información de contacto.</returns>
        public List<ContactClients> GetAllClientContacts(int aplication)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    var clientes = db.Clientes
                        .Where(c => c.aplicacion == aplication)
                        .Select(c => new ContactClients
                        {
                            Cliente1 = c.Cliente1,
                            NombreCompleto = $"{c.PrimerNombre} {c.SegundoNombre ?? ""} {c.TercerNombre ?? ""} {c.PrimerApellido} {c.SegundoApellido}".Trim(),
                            Celular = c.Celular,
                            Celular2 = c.Celular2,
                            Email = c.Email,
                            Nacionalidad = c.Nacionalidad,
                            Notificar = c.Notificar,
                            foto = c.foto
                        })
                        .ToList();

                    return clientes;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la lista de contactos de clientes: {ex.Message}");
            }
        }


        /// <summary>
        /// Crea un nuevo cliente con su información y sube la imagen a Cloudflare si está presente.
        /// </summary>
        /// <param name="newClienteData">El objeto con la información del cliente.</param>
        /// <param name="imageFile">Archivo de imagen enviado desde el frontend.</param>
        /// <param name="userName">Nombre del usuario que crea el cliente.</param>
        /// <param name="idAplicacion">ID de la aplicación.</param>
        /// <returns>El cliente creado con su URL de imagen.</returns>
        public async Task<Cliente> CreateClienteAsync(Cliente newClienteData, IFormFile? imageFile, string userName, int idAplicacion)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // 🔹 Generar un nuevo código de cliente basado en los existentes
                    var lastCliente = db.Clientes
                        .OrderByDescending(c => c.Cliente1)
                        .FirstOrDefault();

                    string newClienteCode = "C00000001"; // Código inicial si no hay registros

                    if (lastCliente != null)
                    {
                        string lastCode = lastCliente.Cliente1.Substring(1);
                        if (int.TryParse(lastCode, out int lastNumber))
                        {
                            newClienteCode = $"C{(lastNumber + 1):D8}";
                        }
                    }

                    // 🔹 Crear objeto Cliente
                    var newCliente = new Cliente
                    {
                        Cliente1 = newClienteCode,
                        PrimerNombre = newClienteData.PrimerNombre,
                        SegundoNombre = newClienteData.SegundoNombre,
                        TercerNombre = newClienteData.TercerNombre,
                        PrimerApellido = newClienteData.PrimerApellido,
                        SegundoApellido = newClienteData.SegundoApellido,
                        Cf = newClienteData.Cf,
                        Dpi = newClienteData.Dpi,
                        Genero = newClienteData.Genero,
                        FechaNacimiento = newClienteData.FechaNacimiento,
                        Celular = newClienteData.Celular,
                        Email = newClienteData.Email,
                        Nit = newClienteData.Nit,
                        NombreFactura = newClienteData.NombreFactura,
                        Direccion = newClienteData.Direccion,
                        Colonia = newClienteData.Colonia,
                        Zona = newClienteData.Zona,
                        Departamento = newClienteData.Departamento,
                        Municipio = newClienteData.Municipio,
                        EstadoCivil = newClienteData.EstadoCivil,
                        Nacionalidad = newClienteData.Nacionalidad,
                        Profesion = newClienteData.Profesion,
                        Empresa = newClienteData.Empresa,
                        Moneda = newClienteData.Moneda,
                        Descuento = newClienteData.Descuento,
                        Activo = true,
                        Edad = newClienteData.Edad,
                        Notificar = newClienteData.Notificar,
                        aplicacion = idAplicacion,
                        Createby = userName,
                        Createdate = DateTime.Now
                    };

                    // 🔹 Subir imagen a Cloudflare si existe
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        string imageUrl = await blUploadImages.UploadImageToCloudflare(imageFile, newClienteCode);
                        newCliente.foto = imageUrl;
                    }

                    db.Clientes.Add(newCliente);
                    await db.SaveChangesAsync();

                    return newCliente;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en CreateCliente: {ex.Message}");
            }
        }


        /// <summary>
        /// Actualiza la información de un cliente existente y sube la imagen si se proporciona.
        /// </summary>
        /// <param name="clienteData">El objeto con la información actualizada del cliente.</param>
        /// <param name="imageFile">Archivo de imagen opcional para actualizar la foto.</param>
        /// <param name="userName">Nombre del usuario que realiza la actualización.</param>
        /// <returns>El cliente actualizado con su nueva información.</returns>
        public async Task<Cliente> UpdateClienteAsync(Cliente clienteData, IFormFile? imageFile, string userName)
        {
            try
            {
                using (SoftByte db = new SoftByte(configuration.appSettings.cadenaSql))
                {
                    // 🔹 Buscar el cliente en la base de datos
                    var existingCliente = await db.Clientes
                        .FirstOrDefaultAsync(c => c.Cliente1 == clienteData.Cliente1);

                    if (existingCliente == null)
                    {
                        throw new Exception($"Cliente con código {clienteData.Cliente1} no encontrado.");
                    }

                    // 🔹 Actualizar los datos del cliente
                    existingCliente.PrimerNombre = clienteData.PrimerNombre;
                    existingCliente.SegundoNombre = clienteData.SegundoNombre;
                    existingCliente.TercerNombre = clienteData.TercerNombre;
                    existingCliente.PrimerApellido = clienteData.PrimerApellido;
                    existingCliente.SegundoApellido = clienteData.SegundoApellido;
                    existingCliente.Cf = clienteData.Cf;
                    existingCliente.Dpi = clienteData.Dpi;
                    existingCliente.Genero = clienteData.Genero;
                    existingCliente.FechaNacimiento = clienteData.FechaNacimiento;
                    existingCliente.Celular = clienteData.Celular;
                    existingCliente.Email = clienteData.Email;
                    existingCliente.Nit = clienteData.Nit;
                    existingCliente.NombreFactura = clienteData.NombreFactura;
                    existingCliente.Direccion = clienteData.Direccion;
                    existingCliente.Colonia = clienteData.Colonia;
                    existingCliente.Zona = clienteData.Zona;
                    existingCliente.Departamento = clienteData.Departamento;
                    existingCliente.Municipio = clienteData.Municipio;
                    existingCliente.EstadoCivil = clienteData.EstadoCivil;
                    existingCliente.Nacionalidad = clienteData.Nacionalidad;
                    existingCliente.Profesion = clienteData.Profesion;
                    existingCliente.Empresa = clienteData.Empresa;
                    existingCliente.Moneda = clienteData.Moneda;
                    existingCliente.Descuento = clienteData.Descuento;
                    existingCliente.Activo = clienteData.Activo;
                    existingCliente.Edad = clienteData.Edad;
                    existingCliente.Notificar = clienteData.Notificar;
                    existingCliente.Updateby = userName;
                    existingCliente.Updatedate = DateTime.Now;

                    // 🔹 Si se proporciona una nueva imagen, subirla y actualizar la URL
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        string imageUrl = await blUploadImages.UploadImageToCloudflare(imageFile, clienteData.Cliente1);
                        existingCliente.foto = imageUrl;
                    }

                    // 🔹 Guardar cambios en la base de datos
                    await db.SaveChangesAsync();

                    return existingCliente;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en UpdateCliente: {ex.Message}");
            }
        }



    }
}
