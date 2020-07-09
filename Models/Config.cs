using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Extensions.Configuration;


namespace Tofree.StockManager.Models
{
    internal class Config
    {
        public static IConfigurationRoot configuration =
            new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@"appsetting.json", false)
            .Build();


    }


}

