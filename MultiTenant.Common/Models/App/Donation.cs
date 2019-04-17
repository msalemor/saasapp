using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenant.Common.Models.App
{
    public class Donation
    {
        public int DonationId { get; set; } 
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime Created { get; set; }
    }
}
