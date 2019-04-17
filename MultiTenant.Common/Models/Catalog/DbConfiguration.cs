using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenant.Common.Models.Catalog
{
    public static class DbConfiguration
    {
        public static string DbUser
        {
            get
            {
                return ConfigurationManager.AppSettings["DbUser"];
            }
        }

        public static string DbPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["DbPassword"];
            }
        }

        public static string ConnectionStringTemplate
        {
            get
            {
                return ConfigurationManager.AppSettings["ConnectionStringTemplate"];
            }
        }

    }
}
