using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.BeneficiaryDetails
{
    public class BeneficiaryDetailsData
    {
        public int totalAllocated { get; set; }
        public int policyNo { get; set; }
        public string auditToken { get; set; }
        public List<BeneficiaryDetailsItems> beneficiaryDetailsItems { get; set; }
    }
}
