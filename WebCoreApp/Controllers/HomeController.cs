using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MultiTenant.Common.Interfaces;
using MultiTenant.Common.Models.App;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebCoreApp.Models;

namespace WebCoreApp.Controllers
{
    public class HomeController : Controller
    {
        readonly IRepository<Donation> repository = new MultiTenant.Common.Repositories.DonationRepository();

        AzureAdB2COptions AzureAdB2COptions;
        public HomeController(IOptions<AzureAdB2COptions> azureAdB2COptions)
        {
            AzureAdB2COptions = azureAdB2COptions.Value;
        }

        public async Task<IActionResult> Index()
        {
            List<Donation> donations = null;
            if (!(Startup.Tenants is null))
            {
                var host = Request.Headers["Host"][0].Split(':')[0];
                var tenant = Startup.Tenants.FirstOrDefault(c => string.Equals(host, c.TenantId, StringComparison.CurrentCultureIgnoreCase));
                if (!(tenant is null))
                {
                    repository.Connection = tenant.ConnectionString;
                    ViewBag.Body = tenant.Body;
                    donations = await repository.GetAllAsync();
                }
            }
            return View(donations);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
