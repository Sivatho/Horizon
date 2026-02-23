using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.AddAdjustementToBillings
{
    // Renamed the class to avoid conflict with the existing definition in the same namespace.
    public class GetOutstandingPolicyPremiumsResponse
    {
        public bool succeeded { get; set; }
        public string message { get; set; }
        public string errors { get; set; }
        public List<GetOutstandingPolicyPremiumsRequest> data { get; set; }
    }
}
