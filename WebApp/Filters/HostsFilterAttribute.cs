using System;
using System.Linq;
using System.Web.Mvc;

namespace WebApp.Filters
{
    public class HostsFilterAttribute : ActionFilterAttribute
    {
        public bool ErrorDetected;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!(MvcApplication.Tenants is null))
            {
                var headers = filterContext.HttpContext.Request.Headers;
                var hostinfo = headers["Host"].Split(':');
                if (hostinfo.Length > 0)
                {
                    var host = hostinfo[0];
                    if (!MvcApplication.Tenants.Any(c => string.Equals(c.TenantId, host, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        //throw new ApplicationException();
                        ErrorDetected = true;
                    }
                } else
                {
                    ErrorDetected = true;
                }
            }
            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (ErrorDetected)
            {
                filterContext.HttpContext.Response.Redirect("/Error/InvalidTenant");
            }
            base.OnResultExecuted(filterContext);
        }
    }
}
