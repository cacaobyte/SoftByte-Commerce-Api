using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CC.Configurations.AppSettings
{
    public class AppSettings
    {
        private IConfiguration appsettingsFile { get; }

        public AppSettings(IConfiguration configuration) {
            appsettingsFile = configuration;
        }
    }
}
