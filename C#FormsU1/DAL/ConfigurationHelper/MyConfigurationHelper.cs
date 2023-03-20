using Microsoft.Extensions.Configuration;
using System.IO;

namespace GameCenter.DAL.ConfigurationHelper
{ //Written with AI assistance
    public static class MyConfigurationHelper
    {
        public static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            return builder.Build();
        }
    }
}
