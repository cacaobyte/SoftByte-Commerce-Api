using CommerceCore.Api.Tools;
using CommerceCore.BL.cc.logistics.warehouse;
using Microsoft.AspNetCore.Mvc;
using CommerceCore.BL.cc.sale.Clientes;
using CommerceCore.ML;

namespace CommerceCore.Api.Controllers.cc.sale.Client
{
    [Route("clients")]
    [ApiController]
    public class ClientController : CustomController
    {
        private Clientes client = new Clientes(Tool.configuration);

        /// <summary>
        /// Devuelve las regiones existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet("allClients")]
        public IActionResult GetAllRegions()
        {
            try
            {
                var result = client.GetClient(userName, IdAplication);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de información de contacto de todos los clientes.
        /// </summary>
        /// <returns>Lista de objetos ContactClients con la información de contacto.</returns>
        [HttpGet("contacts")]
        public IActionResult GetAllClientContacts()
        {
            try
            {
                var result = client.GetAllClientContacts(IdAplication);

                if (result == null || !result.Any())
                {
                    return NotFound(new { message = "No se encontró información de contacto para los clientes." });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }



        /// <summary>
        /// Crea un nuevo cliente con su información y sube su imagen a Cloudflare si es necesario.
        /// </summary>
        /// <param name="newClienteData">Información del cliente.</param>
        /// <param name="imageFile">Imagen del cliente.</param>
        /// <returns>Cliente creado con su URL de imagen.</returns>
        [HttpPost("createClient")]
        public async Task<IActionResult> CreateClient([FromForm] Cliente newClienteData, [FromForm] IFormFile? imageFile)
        {
            try
            {
                if (newClienteData == null)
                {
                    return BadRequest("La información del cliente es obligatoria.");
                }

                var createdClient = await client.CreateClienteAsync(newClienteData, imageFile, userName, IdAplication);
                return Ok(createdClient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el cliente: {ex.Message}");
            }
        }


        /// <summary>
        /// Actualiza la información de un cliente existente y sube su imagen si se proporciona.
        /// </summary>
        /// <param name="updatedClienteData">Información actualizada del cliente.</param>
        /// <param name="imageFile">Nueva imagen del cliente (opcional).</param>
        /// <returns>Cliente actualizado con su URL de imagen.</returns>
        [HttpPut("updateClient")]
        public async Task<IActionResult> UpdateClient([FromForm] Cliente updatedClienteData, [FromForm] IFormFile? imageFile)
        {
            try
            {
                if (updatedClienteData == null || string.IsNullOrEmpty(updatedClienteData.Cliente1))
                {
                    return BadRequest("El código del cliente y la información son obligatorios.");
                }

                var updatedClient = await client.UpdateClienteAsync(updatedClienteData, imageFile, userName);

                return Ok(updatedClient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el cliente: {ex.Message}");
            }
        }



    }
}
