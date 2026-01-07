using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Policy
{
    public class CheckRestartEligibilityRequest
    {
        public int policyNo { get; set; }
        public DateTime billingPeriodToCheck { get; set; }
    }
}
