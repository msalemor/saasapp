using MultiTenant.Common.Models.Catalog;
using MultiTenant.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {

        public static List<Tenant> Tenants;

        protected void Application_Start()
        {
            GetTenantsAsync().ConfigureAwait(true).GetAwaiter();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private async Task GetTenantsAsync()
        {
            var TenantRepository = new TenantRepository();
            Tenants = await TenantRepository.GetAllAsync();
        }
    }

}
