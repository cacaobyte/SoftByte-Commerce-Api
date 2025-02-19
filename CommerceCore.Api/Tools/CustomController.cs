using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CC.Configurations;
using CommerceCore.Api.Attribute;
using CommerceCore.Security;
using CommerceCore.EL;
using System.IdentityModel.Tokens.Jwt;

namespace CommerceCore.Api.Tools
{
    /// <summary>
    /// Cotrolador que se usa como herencia para agregar funcionalidad a los controladores del api
    /// </summary>
    [MiddlewareResponse]
    public class CustomController : ControllerBase
    {
        private UserInfo GetUserInfo()
        {
            string token = HttpContext.Request.Headers["Token"].ToString();

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();

                if (handler.CanReadToken(token))
                {
                    var jwtToken = handler.ReadJwtToken(token);

                    var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
                    var userName = jwtToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
                    var warehouse = jwtToken.Claims.FirstOrDefault(c => c.Type == "celular")?.Value;
                    var aplication = jwtToken.Claims.FirstOrDefault(c => c.Type == "aplication")?.Value;
                    int applicationId = int.TryParse(aplication, out int appId) ? appId : 0;
                    var reqCambioClave = jwtToken.Claims.FirstOrDefault(c => c.Type == "reqCambioClave")?.Value;
                    var fechaUltClave = jwtToken.Claims.FirstOrDefault(c => c.Type == "fechaUltClave")?.Value;
                    var correo = jwtToken.Claims.FirstOrDefault(c => c.Type == "correo")?.Value;
                    var tipoUsuario = jwtToken.Claims.FirstOrDefault(c => c.Type == "tipoUsuario")?.Value;
                    var activo = jwtToken.Claims.FirstOrDefault(c => c.Type == "activo")?.Value;

                    // Retorna un objeto UserInfo personalizado con los nuevos valores
                    return new UserInfo
                    {
                        Username = userName ?? "TestUser",
                        Seller = "DefaultSeller",
                        Store = warehouse ?? "B001",
                        ApplicationId = applicationId, // Se almacena como entero
                        RequiresPasswordChange = reqCambioClave == "True",
                        LastPasswordChangeDate = fechaUltClave ?? "",
                        Email = correo ?? "",
                        UserType = tipoUsuario ?? "Usuario",
                        IsActive = activo == "True"
                    };
                }
            }

            return null;
        }

        /// <summary>
        /// type del usuario
        /// </summary>
        public string Active
        {
            get
            {
                UserInfo userInfo = GetUserInfo();
                return userInfo == null ? "false" : userInfo.IsActive.ToString();
            }
        }

        /// <summary>
        /// type del usuario
        /// </summary>
        public string Usertype
        {
            get
            {
                UserInfo userInfo = GetUserInfo();
                return userInfo == null ? "Usuario" : userInfo.UserType;
            }
        }

        /// <summary>
        /// ID de la aplicación a la cual pertenece el usuario
        /// </summary>
        public int IdAplication
        {
            get
            {
                UserInfo userInfo = GetUserInfo();
                return userInfo?.ApplicationId ?? 0; // Si es nulo, devuelve 0
            }
        }


        /// <summary>
        /// Nombre de usuario conectado al api
        /// </summary>
        public string userName
        {
            get
            {
                UserInfo userInfo = GetUserInfo();
                return userInfo == null ? "TestUser" : userInfo.Username;
            }
        }

        /// <summary>
        /// Código de usuario conectado al api
        /// </summary>
        public string sellerCode
        {
            get
            {
                UserInfo userInfo = GetUserInfo();
                return userInfo == null ? "TestUser" : userInfo.Seller;
            }
        }

        /// <summary>
        /// Tienda de usuario conectado al api
        /// </summary>
        public string storeCode
        {
            get
            {
                UserInfo userInfo = GetUserInfo();
                return userInfo == null ? "TestUser" : userInfo.Store;
            }
        }

        /// <summary>
        /// Token de acceso
        /// </summary>
        public string token => HttpContext.Request.Headers["Token"].ToString();

        /// <summary>
        /// Llave de acceso
        /// </summary>
        public string appKey => HttpContext.Request.Headers["AppKey"].ToString();

        /// <summary>
        /// Crear directorios sino existen
        /// </summary>
        /// <param name="path">Ruta</param>
        internal void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Elimina archivos si existen
        /// </summary>
        /// <param name="filePath">Ruta</param>
        internal void DeleteFileIfExists(string filePath)
        {
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
        }

        /// <summary>
        /// Cargar archivos
        /// </summary>
        /// <param name="postedFiles">Lista de archivos</param>
        /// <param name="folder">Folder</param>
        /// <returns>Lista de archivos</returns>
        internal List<string> UploadingFiles(IFormFileCollection postedFiles, string folder)
        {
            List<string> files = new List<string>();

            postedFiles.ToList().ForEach(x =>
            {
                string extension = Path.GetExtension(x.FileName);
                string file = Path.Combine(folder, x.FileName);
                DeleteFileIfExists(file);

                using FileStream stream = new FileStream(file, FileMode.Create);
                x.CopyTo(stream);
                files.Add(file);
            });

            return files;
        }





    }
}
