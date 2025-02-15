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
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;


namespace CommerceCore.BL.cc.cloudinary
{
    public class UploadImages : LogicBase
    {
        private readonly Account _account;
        private readonly string cloudflareAccountId;
        private readonly string cloudflareApiToken;
        public UploadImages(Configuration settings) 
        {
            configuration = settings;
            // Configuración de la cuenta de Cloudinary
            _account = new Account(
                configuration.cloudinary.Cloudname,
                configuration.cloudinary.APIkey,
                configuration.cloudinary.APIsecret
            );
            cloudflareAccountId = "96860cdf1dd74618ce76b038cb18d733";  
            cloudflareApiToken = "QYTEL3WSBy4Cq2qUvwWNRmRUw2roP-beuBgcPXU_";  
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

        /// <summary>
        /// Subir imagen a Cloudflare Images y devolver la URL.
        /// </summary>
        public async Task<string> UploadImageToCloudflare(IFormFile file, string imageName)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cloudflareApiToken);

                    using (var content = new MultipartFormDataContent())
                    {
                        content.Add(new StreamContent(file.OpenReadStream()), "file", file.FileName);

                        var response = await httpClient.PostAsync(
                            $"https://api.cloudflare.com/client/v4/accounts/{cloudflareAccountId}/images/v1",
                            content
                        );

                        response.EnsureSuccessStatusCode();  // Lanza excepción si no es 2xx

                        var responseBody = await response.Content.ReadAsStringAsync();
                        var jsonResponse = JObject.Parse(responseBody);

                        if (jsonResponse["success"].Value<bool>())
                        {
                            string imageUrl = jsonResponse["result"]["variants"][0].ToString();
                            return imageUrl;
                        }
                        else
                        {
                            throw new Exception($"Error uploading image to Cloudflare: {jsonResponse["errors"][0]["message"]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Cloudflare upload: {ex.Message}");
            }
        }

    }
}
