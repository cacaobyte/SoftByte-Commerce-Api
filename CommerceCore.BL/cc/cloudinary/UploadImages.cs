using System.Collections.Generic;
using System.Linq;
using CC;
using CommerceCore.DAL.Commerce;
using CommerceCore.DAL.Commerce.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using Npgsql.BackendMessages;
using Microsoft.AspNetCore.Http;


namespace CommerceCore.BL.cc.cloudinary
{
    public class UploadImages : LogicBase
    {
        private readonly Account _account;
        public UploadImages(Configuration settings) 
        {
            configuration = settings;
            // Configuración de la cuenta de Cloudinary
            _account = new Account(
                configuration.cloudinary.Cloudname,
                configuration.cloudinary.APIkey,
                configuration.cloudinary.APIsecret
            );

        }


        public string UploadImage(IFormFile file, string folder)
        {
            try
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Folder = folder,
                        UseFilename = true,
                        UniqueFilename = true,
                        Overwrite = false
                    };

                    var cloudinary = new Cloudinary(_account);
                    var uploadResult = cloudinary.Upload(uploadParams);

                    if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return uploadResult.Url.ToString();
                    }
                    else
                    {
                        throw new Exception($"Error uploading image: {uploadResult.Error.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception( ex.Message );
            }
        }



    }
}
