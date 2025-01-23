using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Configurations.AppSettings.service
{
    public class Cloudinary
    {
        private IConfiguration appsettingsFile { get; }

        public Cloudinary(IConfiguration configuration)
        {
            appsettingsFile = configuration;
        }

        public string Cloudname => appsettingsFile.GetValue<string>("Cloudinary:Cloudname") ?? throw new InvalidOperationException("The 'Cloudinary:Cloudname' setting cannot be null or empty.");
        public string APIkey => appsettingsFile.GetValue<string>("Cloudinary:APIkey") ?? throw new InvalidOperationException("The 'Cloudinary:APIkey' setting cannot be null or empty.");
        public string APIsecret => appsettingsFile.GetValue<string>("Cloudinary:APIsecret") ?? throw new InvalidOperationException("The 'Cloudinary:APIsecret' setting cannot be null or empty.");
    }
}
