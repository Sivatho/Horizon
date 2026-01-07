using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.BeneficiaryDetails
{
    public class PolicyBeneficiaryDetailsRequest
    {
        public required int policyNo { get; set; }
        public required string legacyPolicyNumber { get; set; }
        public string auditToken { get; set; }
    }
}
