using MultiTenant.Common.Interfaces;
using MultiTenant.Common.Models.App;
using MultiTenant.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Donation> repository = new DonationRepository();

        public async Task<ActionResult> Index()
        {
            var host = Request.Headers["Host"].Split(':')[0];
            var tenant = MvcApplication.Tenants.FirstOrDefault(c => string.Equals(host, c.TenantId, StringComparison.CurrentCultureIgnoreCase));
            if (tenant==null)
            {
                Response.Redirect("InvalidTenant.html");
            }
            ViewBag.Title = tenant.Title;
            ViewBag.Body = tenant.Body;
            repository.Connection = tenant.ConnectionString;
            var donations = await repository.GetAllAsync();
            return View(donations);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}