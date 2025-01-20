﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CC.Configurations;
using CommerceCore.Api.Attribute;
using CommerceCore.Security;
using CommerceCore.EL;

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
            string appKey = HttpContext.Request.Headers["AppKey"].ToString();
            if (!token.IsNull())
            {
                Dictionary<string, string> header = new Dictionary<string, string>();
                header.Add("Token", token);
                header.Add("AppKey", appKey);
                //Configurar la seguridad para que regrese los datos del usuario de acuerdo a la sesion

            }
            return null;
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
