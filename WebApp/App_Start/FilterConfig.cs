using System.Web.Mvc;
using WebApp.Filters;

namespace WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // If it is not in the valid list of hosts, deny the request
            filters.Add(new HostsFilterAttribute());
        }
    }
}
