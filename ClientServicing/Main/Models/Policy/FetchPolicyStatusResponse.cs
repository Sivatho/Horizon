using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServicing.Main.Models.General;

namespace ClientServicing.Main.Models.Policy
{
    public class FetchPolicyStatusResponse
    {
        public ExecutionOutcome executionOutcome { get; set; }
        public PolicyStatus data { get; set; }
    }

    public class PolicyStatus
    {
        public string legacyPolicyNo { get; set; }
        public int policyNo { get; set; }
        public string status { get; set; }
        public int statusCD { get; set; }
        public DateTime statusDate { get; set; }
    }
}
