using CC.Configurations.AppSettings;
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

    }
}
