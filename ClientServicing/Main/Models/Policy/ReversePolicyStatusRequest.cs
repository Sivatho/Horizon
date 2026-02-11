using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Policy
{
    public class ReversePolicyStatusRequest
    {
        public int policyNo { get; set; }
        public string effectiveDate { get; set; }
        public string noteText { get; set; }
    }
}
