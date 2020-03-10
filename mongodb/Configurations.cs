using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace mongodb
{
    public interface IAppConfigurations
    {
        string MongoDbConnectionString();
        string Version();
        string MongoDbDatabase();
    }

    public class AppConfigurations : IAppConfigurations
    {
        public string MongoDbConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString;
        }

        public string Version()
        {
            return ConfigurationManager.AppSettings.Get("Version");
        }

        public string MongoDbDatabase()
        {
            return ConfigurationManager.AppSettings.Get("MongoDbDatabase");
        }
    }
}
