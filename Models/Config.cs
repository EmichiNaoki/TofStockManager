using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Extensions.Configuration;


namespace Tofree.StockManager.Models
{
    internal class Config
    {
        public static IConfigurationRoot configuration;
        public static void Main(string[] args)
        {

            configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@"jsconfig.json", false).Build();
            
        }

    }


}

