using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Policy
{
    public class GetCustomerPolicyInfoByEntityNoRequest
    {
        public string entityNo { get; set; }
        public int partnerCd { get; set; }
        public string auditToken { get; set; }
    }
}
