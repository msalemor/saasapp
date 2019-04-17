using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenant.Common.Models.Catalog
{
    public class Tenant
    {
        public string TenantId { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public int Port { get; set; }
        public DateTime Created { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public string Footer { get; set; }
        public string Title { get; set; }
        public string ConnectionString { get; set; }
    }
}
