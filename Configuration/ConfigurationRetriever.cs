using Microsoft.Extensions.Configuration;
using System;

namespace BikeStore.Configuration
{
    /// <summary>
    /// ConfigurationRetriever will be used to retrieve any configurations from the appsettings.json file.
    /// 
    /// </summary>
    public class ConfigurationRetriever
    {
        private IConfiguration _config { get; set; }

        public ConfigurationRetriever()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            _config = config;
        }
        public ConfigurationRetriever(IConfiguration config)
        {
            _config = config;
        }

        public String RetrieveConfig(string connection)
        {
            string returnObj = _config[connection];
            return returnObj ?? string.Empty;
        }

        public String RetrieveConfig(string key, string value)
        {
            string returnObj = _config[key + ":" + value];

            return returnObj ?? string.Empty;
        }
    }
}
