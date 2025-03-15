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


    }
}
