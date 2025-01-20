using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using CC;
namespace CommerceCore.Api
{
    public class Tool {
        private static IConfiguration appSettings { get; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
        .AddEnvironmentVariables().Build();

        public static Configuration configuration { get; } = new Configuration(appSettings);
    }
}
