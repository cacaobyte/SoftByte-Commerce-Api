using CC.Configurations.AppSettings;
using CC.Configurations.AppSettings.service;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC
{
    public class Configuration
    {
        private IConfiguration appsettingsFile { get; }


        public Configuration(IConfiguration configuration)
        {
            appsettingsFile = configuration;
        }

        public AppSettings appSettings => new AppSettings(this.appsettingsFile);

        public Cloudinary cloudinary => new Cloudinary(this.appsettingsFile);

    }
}
