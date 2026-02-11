using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.General
{
    public class PolicyNoAndEffectiveDate
    {
        public int policyNo { get; set; }
        public DateTime effectiveDate { get; set; }
        public string auditToken { get; set; }
    }
}
