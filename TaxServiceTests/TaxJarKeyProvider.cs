using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TaxServiceTests
{
    internal class TaxJarKeyProvider
    {
        internal string GetAPIKey()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<TaxJarKeyProvider>()
                .Build();

            return config["TaxJarApiKey"];
        }


    }
}
